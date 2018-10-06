using Systems.AttributeSystem;
using TMPro;
using UnityEngine;

namespace Systems.UiSystem
{
    //todo wrapper is mainly for later, mouse over stuff etc
    public class TowerInfoElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textElement;
        private Attribute attribute;

        public void InitTowerInfoElement(Attribute attribute)
        {
            var str = attribute.AttributeName + " " + attribute.Value.ToString("F2");
            var lvlInc = attribute.LevelIncrement * 100;
            var typeStr = (attribute.LevelIncrementType == LevelIncrementType.Percentage) ? "% / Level)" : ")";

            if (lvlInc > 0.0f)
            {
                str += " (+" + lvlInc + typeStr;
            }

            textElement.text = str;
        }
    }
}