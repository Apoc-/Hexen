using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Hexen
{
    public class Projectile : Entity
    {
        public float Speed;
        public Npc Target;

        private void FixedUpdate()
        {
            if (this.Target == null) return;

            Vector3 target = Target.transform.position;
            Vector3 position = this.transform.position;

            Vector3 direction = target - position;
            direction.Normalize();

            transform.SetPositionAndRotation(
                position + direction * (Speed * Time.fixedDeltaTime),
                Quaternion.LookRotation(direction, Vector3.up));
        }
    }
}
