using UnityEngine;

namespace Assets.Scripts.Systems.HandSystem
{
    public class HiredHandButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonActiveEffect;

        public HiredHand Hand { get; set; }
    }
}