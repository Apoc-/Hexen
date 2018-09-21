using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.MapSystem;
using Assets.Scripts.Systems.SfxSystem;
using Assets.Scripts.Systems.TowerSystem;
using Assets.Scripts.Systems.WaveSystem;
using UnityEngine;
using UnityEngine.Events;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Npcs
{
    public abstract class Npc : MonoBehaviour, IHasAttributes, AttributeEffectSource, AuraTarget
    {
        public AttributeContainer Attributes;

        public string Name;
        public GameObject Model;
        public Tile Target;
        public Tile CurrentTile;
        public Wave SpawnedInWave = null;

        public bool isSpawned = false;

        [SerializeField]
        private float currentHealth;

        public float CurrentHealth
        {
            get { return currentHealth; }
            private set { currentHealth = value; }
        }

        public int Level = 1;
        private bool shouldDie = false;

        protected NpcHealthBar healthBar;
        protected float HealthBarOffset = 0.0f;
        protected float HealthBarScale = 1.0f;

        private List<NpcDot> dots = new List<NpcDot>();
        private float tickTime = 0;
        private float dotCheckInterval = 0.1f;

        public Rarities Rarity;
        public FactionNames Faction = FactionNames.Humans;

        //events
        public delegate void NpcHitEvent(NpcHitData hitData, Npc npc);
        public event NpcHitEvent OnHit;

        public delegate void NpcDeathEvent(Npc npc);
        public event NpcDeathEvent OnDeath;


        public void InitData()
        {
            this.InitAttributes();
            this.InitNpcData();
        }

        public void InitVisuals()
        {
            this.InitNpcModel();

            this.CurrentHealth = Attributes[AttributeName.MaxHealth].Value;
            this.InitHealthBar();

            InvokeRepeating("RemoveFinishedTimedAttributeEffects", 0, 1);
        }

        private void InitHealthBar()
        {
            var barPrefab = Resources.Load("Prefabs/UI/NpcHealthBar");
            var bar = (GameObject) Instantiate(barPrefab);
            bar.transform.parent = this.transform;

            var pos = Vector3.zero;
            pos.y = pos.y + this.HealthBarOffset;
            bar.transform.localPosition = pos;

            healthBar = bar.GetComponent<NpcHealthBar>();

            bar.transform.localScale *= HealthBarScale;

            healthBar.UpdateHealth(1.0f);
        }

        protected void Update()
        {
            if (shouldDie)
            {

                Die();
            }

            tickTime += Time.deltaTime;
            if (tickTime >= dotCheckInterval)
            {
                DoDots();
                tickTime = 0.0f;
            }
        }

        private void DoDots()
        {
            dots.RemoveAll(dot => dot.IsFinished());

            dots.ForEach(dot =>
            {
                dot.IncreaseActiveTime(Time.deltaTime);

                if (dot.ShouldTick(Time.time))
                {
                    this.DealDamage(dot.Damage, dot.Source);
                    dot.DoTick(Time.time);
                }
            });
        }

        protected abstract void InitNpcData();

        public void InitNpcModel()
        {
            var mdlGo = Instantiate(Model);
            mdlGo.transform.SetParent(transform, false);
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
        }

        public void AddAttribute(Attribute attr)
        {
            Attributes.AddAttribute(attr);
        }

        public Attribute GetAttribute(AttributeName attrName)
        {
            return Attributes[attrName];
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
                this.LevelUp();
            }

            this.CurrentHealth = Attributes[AttributeName.MaxHealth].Value;
        }

        public virtual void HitNpc(NpcHitData hitData)
        {
            if (OnHit != null) OnHit(hitData, this);

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
                shouldDie = true;
            }

            var maxHealth = Attributes[AttributeName.MaxHealth].Value;
            healthBar.UpdateHealth(CurrentHealth / maxHealth);
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

            healthBar.UpdateHealth(CurrentHealth / maxHealth);
        }

        private void GiveRewards(Tower source)
        {
            if (this.HasAttribute(AttributeName.XPReward))
            {
                GiveXP(source, this.GetAttribute(AttributeName.XPReward).Value);
            }

            if (this.HasAttribute(AttributeName.GoldReward))
            {
                GiveGold(source.Owner, this.GetAttribute(AttributeName.GoldReward).Value);
            }
        }

        private void GiveXP(Tower target, float amount)
        {
            float factor = (Mathf.Pow(GameSettings.NpcGoldFactorExpBase, (int)Rarity));
            amount *= factor;
            target.GiveXP((int)amount);
        }

        private void GiveGold(Player target, float amount)
        {
            float factor = (Mathf.Pow(GameSettings.NpcXpFactorExpBase, (int)Rarity));
            amount *= factor;
            target.IncreaseGold((int)amount);
        }

        public virtual void Die()
        {
            if (OnDeath != null) OnDeath.Invoke(this);

            this.isSpawned = false;
            this.SpawnedInWave.SpawnedNpcs.Remove(this);
            Explode();
        }

        void Explode()
        {
            var specialEffect = new SpecialEffect(
                effectPrefabName: "BloodEffect",
                origin: gameObject,
                duration: 10f,
                followsOrigin: false,
                diesWithOrigin: false);

            GameManager.Instance.SfxManager.PlaySpecialEffect(specialEffect);

            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if (this.Target == null)
            {
                this.Target = GameManager.Instance.MapManager.StartTile;
                transform.position = Target.GetTopCenter();
            }

            Vector3 target = Target.GetTopCenter();
            Vector3 position = this.transform.position;

            Vector3 direction = target - position;

            var speed = Attributes[AttributeName.MovementSpeed].Value;

            if (direction.magnitude < (speed * Time.fixedDeltaTime * 0.8f))
            {
                EnterTile(Target);
                Target = GameManager.Instance.MapManager.GetNextTileInPath(Target);
            }

            direction.Normalize();

            if (direction.magnitude > 0.0001f)
            {
                transform.SetPositionAndRotation(
                    position + direction * (speed * Time.fixedDeltaTime),
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
            foreach (var pair in this.Attributes)
            {
                pair.Value.RemovedFinishedAttributeEffects();
            }
        }

        public void ApplyDot(NpcDot dot)
        {
            //only one dot per source for now
            if (dots.Any(d => d.Source == dot.Source)) return;

            this.dots.Add(dot);
        }

        //todo refactor, tower has the same 
        public List<Collider> GetCollidersInRadius(float radius)
        {
            var baseHeight = GameManager.Instance.MapManager.BaseHeight;

            var topCap = transform.position + new Vector3(0, 5, 0);
            var botCap = new Vector3(transform.position.x, baseHeight - 5, transform.position.z);

            return new List<Collider>(Physics.OverlapCapsule(topCap, botCap, radius));
        }

        protected List<Tower> GetTowersInRadius(float radius)
        {
            return GetCollidersInRadius(radius)
                .Select(col => col.GetComponentInParent<Tower>())
                .Where(go => go != null)
                .ToList();
        }
    }
}
