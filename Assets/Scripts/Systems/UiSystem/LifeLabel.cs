using Assets.Scripts.Systems.GameSystem;

namespace Assets.Scripts.Systems.UiSystem
{
    public class LifeLabel : StatLabel
    {
        protected override string GetValue()
        {
            return GameManager.Instance.Player.Lives  + "";
        }
    }
}