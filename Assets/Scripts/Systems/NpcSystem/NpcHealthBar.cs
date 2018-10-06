using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Systems.NpcSystem
{
    public class NpcHealthBar : MonoBehaviour
    {
        [FormerlySerializedAs("health")] [SerializeField]
        private Image _health;

        public void Update()
        {
            //transform.LookAt(Vector3.up);
        }

        public void UpdateHealth(float percent)
        {
            if (_health == null) return;

            var rect = _health.gameObject.GetComponent<RectTransform>();
            rect.anchorMax = new Vector2(percent, 1.0f);
        }
    }
}
