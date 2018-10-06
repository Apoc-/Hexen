using System.Collections.Generic;
using System.Linq;
using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.MapSystem;
using Systems.SpecialEffectSystem;
using Systems.TowerSystem;
using Systems.WaveSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Attribute = Systems.AttributeSystem.Attribute;

namespace Systems.NpcSystem
{
    public abstract class Npc : MonoBehaviour, IHasAttributes, IAttributeEffectSource, IAuraTarget
    {
        public AttributeContainer Attributes;

        public string Name;
        protected GameObject ModelPrefab;
        private GameObject _modelGameObject;
        public Tile Target;
        public Tile CurrentTile;
        public Wave SpawnedInWave;

        [FormerlySerializedAs("isSpawned")] public bool IsSpawned;

        [FormerlySerializedAs("currentHealth")] [SerializeField]
        private float _currentHealth;

        public float CurrentHealth
        {
            get { return _currentHealth; }
            private set { _currentHealth = value; }
        }

        public int Level = 1;
        protected bool ShouldDie;

        protected NpcHealthBar HealthBar;
        protected float HealthBarOffset = 0.0f;
        protected float HealthBarScale = 1.0f;

        private List<NpcDot> _dots = new List<NpcDot>();
        private float _tickTime;
        private float _dotCheckInterval = 0.1f;

        public Rarities Rarity;
        public FactionNames Faction = FactionNames.Humans;
        private Tower _killer;

        //events
        public delegate void NpcHitEvent(Npc sender, NpcHitData hitData);
        public event NpcHitEvent OnHit;

        public delegate void NpcDeathEvent(Npc sender, Tower killer);
        public event NpcDeathEvent OnDeath;


        public void InitData()
        {
            InitAttributes();
            InitNpcData();
        }

        public void InitVisuals()
        {
            LoadNpcModel();

            CurrentHealth = Attributes[AttributeName.MaxHealth].Value;
            InitHealthBar();

            InvokeRepeating("RemoveFinishedTimedAttributeEffects", 0, 1);
        }

        private void InitHealthBar()
        {
            var barPrefab = Resources.Load("Prefabs/UI/NpcHealthBar");
            var bar = (GameObject) Instantiate(barPrefab);
            bar.transform.SetParent(transform);

            var pos = Vector3.zero;
            pos.y = pos.y + HealthBarOffset;
            bar.transform.localPosition = pos;

            HealthBar = bar.GetComponent<NpcHealthBar>();

            bar.transform.localScale *= HealthBarScale;

            HealthBar.UpdateHealth(1.0f);
        }

        protected void Update()
        {
            if (ShouldDie)
            {

                Die();
            }

            _tickTime += Time.deltaTime;
            if (_tickTime >= _dotCheckInterval)
            {
                DoDots();
                _tickTime = 0.0f;
            }
        }

        private void DoDots()
        {
            _dots.RemoveAll(dot => dot.IsFinished());

            _dots.ForEach(dot =>
            {
                dot.IncreaseActiveTime(Time.deltaTime);

                if (dot.ShouldTick(Time.time))
                {
                    DealDamage(dot.Damage, dot.Source);
                    dot.DoTick(Time.time);
                }
            });
        }

        protected abstract void InitNpcData();

        public void LoadNpcModel()
        {
            if (_modelGameObject != null) return; //already loaded

            _modelGameObject = Instantiate(ModelPrefab);
            _modelGameObject.transform.SetParent(transform, false);
        }

        public void ReloadNpcModel()
        {
            Destroy(_modelGameObject);

            _modelGameObject = Instantiate(ModelPrefab);
            _modelGameObject.transform.SetParent(transform, false);
        }

        protected virtual void InitAttributes()
        {
            Attributes = new AttributeContainer();

            AddAttribute(new Attribute(
                AttributeName.GoldReward, 
                GameSettings.NpcGoldBase, 
                GameSettings.NpcGoldInc, 
                LevelIncrementType.Flat));

            AddAttribute(new Attribute(
                AttributeName.XPReward, 
                GameSettings.NpcXPBase,
                GameSettings.NpcXPInc,
                LevelIncrementType.Flat));

            AddAttribute(new Attribute(AttributeName.XPRewardFactor, 1));
            AddAttribute(new Attribute(AttributeName.GoldRewardFactor, 1));
        }

        public void AddAttribute(Attribute attr)
        {
            Attributes.AddAttribute(attr);
        }

        public Attribute GetAttribute(AttributeName attrName)
        {
            return Attributes[attrName];
        }

        public float GetAttributeValue(AttributeName attrName)
        {
            return Attributes[attrName].Value;
        }


        public bool HasAttribute(AttributeName attrName)
        {
            return Attributes.HasAttribute(attrName);
        }

        private void LevelUp()
        {
            Level += 1;

            foreach (var kvp in Attributes)
            {
                kvp.Value.LevelUp();
            }
        }

        public void SetLevel(int lvl)
        {
            for (int i = 1; i < lvl; i++)
            {
                LevelUp();
            }

            CurrentHealth = Attributes[AttributeName.MaxHealth].Value;
        }

        public virtual void HitNpc(NpcHitData hitData)
        {
            OnHit?.Invoke(this, hitData);

            DealDamage(hitData.Dmg, hitData.Source);
        }

        public virtual void DealDamage(float dmg, Tower source)
        {
            var actualDmg = dmg;
            if (HasAttribute(AttributeName.AbsoluteDamageReduction))
            {
                actualDmg -= Attributes[AttributeName.AbsoluteDamageReduction].Value;
                if (actualDmg <= 0) return;
            }

            CurrentHealth -= actualDmg;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                GiveRewards(source);
                _killer = source;
                ShouldDie = true;
            }

            var maxHealth = Attributes[AttributeName.MaxHealth].Value;
            HealthBar.UpdateHealth(CurrentHealth / maxHealth);
        }


        public void Heal(float amount = -1)
        {
            var maxHealth = Attributes[AttributeName.MaxHealth].Value;
            //negative heal / full heal / no overheal
            if (amount <= -1 || CurrentHealth + amount > maxHealth)
            {
                CurrentHealth = maxHealth;
            }
            else
            {
                CurrentHealth += amount;
            }

            HealthBar.UpdateHealth(CurrentHealth / maxHealth);
        }

        private void GiveRewards(Tower source)
        {
            if (HasAttribute(AttributeName.XPReward))
            {
                GiveXP(source, GetAttribute(AttributeName.XPReward).Value);
            }

            if (HasAttribute(AttributeName.GoldReward))
            {
                GiveGold(source.Owner, GetAttribute(AttributeName.GoldReward).Value);
            }
        }

        private void GiveXP(Tower target, float amount)
        {
            float factor = (Mathf.Pow(GameSettings.NpcXpFactorExpBase, (int) Rarity-1));

            if (Rarity == Rarities.None) factor = 1;
            
            amount *= factor;
            amount *= GetAttributeValue(AttributeName.XPRewardFactor);

            if (amount < 1) amount = 1;

            target.GiveXP((int)amount);
        }

        private void GiveGold(Player target, float amount)
        {
            float factor = (Mathf.Pow(GameSettings.NpcGoldFactorExpBase, (int) Rarity-1 ));

            if (Rarity == Rarities.None) factor = 1;

            amount *= factor;
            amount *= GetAttributeValue(AttributeName.GoldRewardFactor);

            if (amount < 1) amount = 1;

            target.IncreaseGold((int)amount);
        }

        public virtual void Die(bool silent = false)
        {
            OnDeath?.Invoke(this, _killer);

            IsSpawned = false;
            SpawnedInWave.SpawnedNpcs.Remove(this);

            if (!silent)
            {
                Splatter();
            }
            
            Destroy(gameObject);
        }

        protected void Splatter()
        {
            var specialEffect = new ParticleEffectData(
                effectPrefabName: "BloodEffect",
                origin: gameObject,
                duration: 3f,
                followsOrigin: false,
                diesWithOrigin: false);

            GameManager.Instance.SpecialEffectManager.PlayParticleEffect(specialEffect);
        }

        /*bool measured = false;*/
        protected virtual void FixedUpdate()
        {
            
            /*if (!measured)
            {
                GetPositionInTime(1 / Attributes[AttributeName.MovementSpeed].Value);
                measured = true;
                timer = 0;
            }

            if (measured)
            {
                timer += Time.fixedDeltaTime;
            }

            if (timer >= eta)
            {
                Debug.Log("-------------");
                Debug.Log("Est pos: " + futurePos);
                Debug.Log("Actual pos: " + transform.position);
                Debug.Log("Dif " + Vector3.Distance(futurePos, transform.position));
                Debug.Log("-------------");

                Debug.DrawLine(futurePos, transform.position, Color.green, 30);
                if (CurrentTile != null)
                {
                    Debug.DrawLine(CurrentTile.GetTopCenter(), CurrentTile.GetTopCenter() + Vector3.up, Color.red);
                }

                if (Target != null)
                {
                    Debug.DrawLine(Target.GetTopCenter(), Target.GetTopCenter() + Vector3.up, Color.red);
                }
                measured = false;
            }*/


            if (Target == null)
            {
                Target = GameManager.Instance.MapManager.StartTile;
                transform.position = Target.GetTopCenter();
            }

            Vector3 target = Target.GetTopCenter();
            Vector3 position = transform.position;
            Vector3 distance = target - position;

            if (distance.magnitude < 0.1f)
            {
                EnterTile(Target);
                
                Target = GameManager.Instance.MapManager.GetNextTileInPath(Target);
            }

            var speed = Attributes[AttributeName.MovementSpeed].Value;
            var direction = distance.normalized;

            if (direction.magnitude >= 0.0001f)
            {
                transform.SetPositionAndRotation(
                    position + direction * speed * Time.fixedDeltaTime,
                    Quaternion.LookRotation(direction, Vector3.up));
            }

        }

        private void EnterTile(Tile tile)
        {
            if (tile != CurrentTile)
            {
                CurrentTile = tile;
                CheckEndTile(tile);
            }

            tile.EnterTile(this);
        }

        private void CheckEndTile(Tile tile)
        {
            if (tile == GameManager.Instance.MapManager.EndTile)
            {
                GameManager.Instance.Player.Lives -= 1;
            }
        }

        public void RemoveFinishedTimedAttributeEffects()
        {
            foreach (var pair in Attributes)
            {
                pair.Value.RemovedFinishedAttributeEffects();
            }
        }

        public void ApplyDot(NpcDot dot)
        {
            //only one dot per source for now
            if (_dots.Any(d => d.Source == dot.Source)) return;

            _dots.Add(dot);
        }


        public Vector3 GetPositionInTime(float time)
        {
            if (CurrentTile == null || Target == null) return transform.position;
            if (transform == null) return Vector3.zero;

            var velocity = Attributes[AttributeName.MovementSpeed].Value;
            var totalDistance = velocity * time;

            Vector3 currentPosition = transform.position;
            Tile next = Target;
            float distanceLeft = totalDistance;
            
            while (true)
            {
                //todo check end
                //if (current.TileType == TileType.End) return current.GetTopCenter();

                //walk distance
                var dist = Vector3.Distance(currentPosition, next.GetTopCenter());

                //check if target can still be reached
                if (distanceLeft - dist < 0) break;

                distanceLeft -= dist;

                //next target
                currentPosition = next.GetTopCenter();
                next = GameManager.Instance.MapManager.GetNextTileInPath(next);
            }
            

            //walk leftover distance
            var heading = next.GetTopCenter() - currentPosition;
            var pos = currentPosition + heading.normalized * distanceLeft;

            return pos;
        }
    }
}
