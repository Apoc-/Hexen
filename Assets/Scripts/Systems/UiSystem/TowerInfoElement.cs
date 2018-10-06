using Systems.AttributeSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems.UiSystem
{
    //todo wrapper is mainly for later, mouse over stuff etc
    public class TowerInfoElement : MonoBehaviour
    {
        [FormerlySerializedAs("textElement")] [SerializeField] private TextMeshProUGUI _textElement;
        private Attribute _attribute;

        public void InitTowerInfoElement(Attribute attribute)
        {
            var str = attribute.AttributeName + " " + attribute.Value.ToString("F2");
            var lvlInc = attribute.LevelIncrement * 100;
            var typeStr = (attribute.LevelIncrementType == LevelIncrementType.Percentage) ? "% / Level)" : ")";

            if (lvlInc > 0.0f)
            {
                str += " (+" + lvlInc + typeStr;
            }

            _textElement.text = str;
        }
    }
}