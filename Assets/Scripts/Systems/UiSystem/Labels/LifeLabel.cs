using Systems.GameSystem;

namespace Systems.UiSystem.Labels
{
    public class LifeLabel : StatLabel
    {
        protected override string GetValue()
        {
            return GameManager.Instance.Player.Lives  + "";
        }
    }
}