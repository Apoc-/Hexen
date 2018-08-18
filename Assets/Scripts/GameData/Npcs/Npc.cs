using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.ProjectileSystem;
using Hexen;
using Hexen.AttributeSystem;
using Hexen.GameData.Towers;
using UnityEngine;
using UnityScript.Steps;

namespace Hexen
{
    public abstract class Npc : MonoBehaviour, IHasAttributes, AttributeEffectSource
    {
        public AttributeContainer attributes;

        public string Name;
        public GameObject Model;
        public Tile Target;
        public Tile CurrentTile;

        public int Level = 1;

        void Awake()
        {
            this.InitAttributes();
            this.InitNpc();
            this.InitNpcModel();
        }

        protected abstract void InitNpc();

        public void InitNpcModel()
        {
            var mdlGo = Instantiate(Model);
            mdlGo.transform.SetParent(transform, false);
        }

        protected virtual void InitAttributes()
        {
            attributes = new AttributeContainer();
        }

        public void AddAttribute(Attribute attr)
        {
            attributes.AddAttribute(attr);
        }

        public Attribute GetAttribute(AttributeName attrName)
        {
            return attributes[attrName];
        }

        public bool HasAttribute(AttributeName attrName)
        {
            return attributes.HasAttribute(attrName);
        }

        private void LevelUp()
        {
            Level += 1;

            foreach (var kvp in attributes)
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

        public void DealDamage(float dmg)
        {
            var dmgEffect = new AttributeEffect(-dmg, AttributeName.Health, AttributeEffectType.Flat, this);
            attributes[AttributeName.Health].AddAttributeEffect(dmgEffect);
        }


        public void Kill(bool forcefully = false)
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
            
            var collider = GetComponentInChildren<Collider>();
            var renderer = GetComponentInChildren<Renderer>();

            //todo fix, produced bugs, no blood for now
            /*var exp = GetComponentInChildren<ParticleSystem>();
            exp.Play();

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

            var speed = attributes[AttributeName.MovementSpeed].Value;

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

    }
}
