using Assets.Scripts.Systems.ProjectileSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ProjectileModel : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GetComponentInParent<Projectile>().Collide(other);
        }
    }
}