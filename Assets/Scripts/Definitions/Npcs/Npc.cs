using System.Collections.Generic;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.MapSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Npcs
{
    public abstract class Npc : MonoBehaviour, IHasAttributes, AttributeEffectSource
    {
        public AttributeContainer Attributes;

        public string Name;
        public GameObject Model;
        public Tile Target;
        public Tile CurrentTile;
        public int CurrentHealth { get; private set; }

        public int Level = 1;
        private bool shouldDie = false;

        private NpcHealthBar healthBar;
        protected float HealthBarOffset = 0.0f;
        protected float HealthBarScale = 1.0f;

        void Awake()
        {
            this.InitAttributes();
            this.InitNpc();
            this.InitNpcModel();

            this.CurrentHealth = (int) Attributes[AttributeName.MaxHealth].Value;
            this.InitHealthBar();

            InvokeRepeating("RemoveFinishedTimedAttributeEffects", 0, 1);
        }

        private void InitHealthBar()
        {
            var barPrefab = Resources.Load("Prefabs/UI/NpcHealthBar");
            var bar = (GameObject) Instantiate(barPrefab);
            bar.transform.parent = this.transform;

            var pos = bar.transform.position;
            pos.y = pos.y + this.HealthBarOffset;
            bar.transform.position = pos;


            healthBar = bar.GetComponent<NpcHealthBar>();

            bar.transform.localScale *= HealthBarScale;

            healthBar.UpdateHealth(1.0f);
        }

        void Update()
        {
            if (shouldDie)
            {
                Die();
            }
        }

        protected abstract void InitNpc();

        public void InitNpcModel()
        {
            var mdlGo = Instantiate(Model);
            mdlGo.transform.SetParent(transform, false);
        }

        protected virtual void InitAttributes()
        {
            Attributes = new AttributeContainer();
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
        }

        public void DealDamage(float dmg, Tower source)
        {
            CurrentHealth -= (int) dmg;
            
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                GiveRewards(source);
                shouldDie = true;
            }

            var maxHealth = Attributes[AttributeName.MaxHealth].Value;
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
            target.GiveXP((int)amount);
        }

        private void GiveGold(Player target, float amount)
        {
            target.IncreaseGold((int)amount);
        }


        public void Die(bool forcefully = true)
        {
            if (forcefully)
            {
                Explode();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Explode()
        {
            GameManager.Instance.SfxManager.PlaySpecialEffect(this, "Blood");
            /*
            if a tower shoots at the moment this explodes => null ref in firing logic
            if (collider != null) Destroy(collider);
            if (renderer != null) Destroy(renderer);
            Destroy(gameObject, exp.main.duration);*/

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

        //todo refactor, tower has the same 
        public List<Collider> GetCollidersInRadius(float radius)
        {
            var baseHeight = GameManager.Instance.MapManager.BaseHeight;

            var topCap = transform.position + new Vector3(0, 5, 0);
            var botCap = new Vector3(transform.position.x, baseHeight - 5, transform.position.z);

            return new List<Collider>(Physics.OverlapCapsule(topCap, botCap, radius));
        }

    }
}
