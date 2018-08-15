using Hexen;
using Hexen.GameData.Runes;
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
            Rune.ApplyRune(GameManager.Instance.TowerSelectionManager.CurrentSelectedTower);
        }

        void DebugRemoveRune()
        {
            Rune.RemoveRune();
        }
    }
}