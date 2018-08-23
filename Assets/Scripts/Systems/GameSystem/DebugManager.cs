using Hexen;
using Hexen.GameData.Runes;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Assets.Scripts.Managers
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
            Rune.ApplyRune(twr);
        }

        void DebugRemoveRune()
        {
            Rune.RemoveRune();
        }
    }
}