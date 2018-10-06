using Systems.GameSystem;
using TMPro;
using UnityEngine;

namespace Systems.UiSystem.Labels
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