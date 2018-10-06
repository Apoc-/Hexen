using UnityEngine;

namespace Definitions.ProjectileAttacks
{
    public class ProjectileModel : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var layer1 = other.gameObject.layer;
            if (Physics.GetIgnoreLayerCollision(layer1, gameObject.layer)) return;

            //GetComponentInParent<ProjectileAttack>().Collide(other);
        }
    }
}