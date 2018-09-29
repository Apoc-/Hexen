using Assets.Scripts.Systems.GameSystem;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Systems.UiSystem
{
    public class WaveTimer : MonoBehaviour
    {
        void Update()
        {
            var spawner = GameManager.Instance.WaveSpawner;
            var displayTime = spawner.WaveCooldown - spawner.CurrentElapsedTime;
            GetComponentInChildren<TextMeshProUGUI>().text = "" + displayTime;
        }
    }
}