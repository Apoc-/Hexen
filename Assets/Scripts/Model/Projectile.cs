using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen.GameData.Towers;
using UnityEngine;
using Random = System.Random;

namespace Hexen
{
    public class Projectile : Entity
    {
        public Tower Source;
        public float Speed;
        public Npc Target;

        private void Update()
        {
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collide(collision);
        }

        protected virtual void Collide(Collision collision)
        {
            var npc = collision.gameObject.GetComponentInParent<Npc>();

            if (npc != null)
            {
                var factor = 1;

                if (CheckCrit())
                {
                    factor = 2;
                }
                npc.DealDamage(this, factor);
                Destroy(gameObject);
            }
        }

        private bool CheckCrit()
        {
            var crit = this.Source.GetAttribute(AttributeName.CritChance);

            Random r = new Random();
            var n = (float) r.NextDouble();

            return n <= crit.Value;
        }

        private void FixedUpdate()
        {
            if (this.Target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 target = Target.GetComponentInChildren<Collider>().transform.position;
            Vector3 position = this.transform.position;

            Vector3 direction = target - position;
            direction.Normalize();

            transform.SetPositionAndRotation(
                position + direction * (Speed * Time.fixedDeltaTime),
                Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(90,0,0));
        }
    }
}
