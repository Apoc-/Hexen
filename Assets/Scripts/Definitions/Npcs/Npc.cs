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

        void Awake()
        {
            this.InitAttributes();
            this.InitNpc();
            this.InitNpcModel();

            this.CurrentHealth = (int) Attributes[AttributeName.MaxHealth].Value;

            InvokeRepeating("RemoveFinishedTimedAttributeEffects", 0, 1);
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
                GiveRewards(source);
                shouldDie = true;
            }
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

    }
}
