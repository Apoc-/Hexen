using Assets.Scripts.Definitions.Runes;
using UnityEngine;

namespace Assets.Scripts.Systems.GameSystem
{
    public class DebugManager : MonoBehaviour
    {
        public Rune Rune;

        void Start()
        {
            Rune = new RuneA();
            Rune.InitRuneData();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                DebugAddRune();
            }

            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                DebugRemoveRune();
            }
        }

        void DebugAddRune()
        {
            var twr = GameManager.Instance.TowerSelectionManager.CurrentSelectedTower;
            //Rune.ApplyRune(twr);
        }

        void DebugRemoveRune()
        {
            Rune.RemoveRune();
        }
    }
}