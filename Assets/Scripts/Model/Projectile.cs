using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
            var npc = collision.gameObject.GetComponentInParent<Npc>();

            if (npc != null)
            {
                npc.DealDamage(Source.AttackDamage);
            }

            Destroy(this.gameObject);
        }

        private void FixedUpdate()
        {
            if (this.Target == null) return;

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
