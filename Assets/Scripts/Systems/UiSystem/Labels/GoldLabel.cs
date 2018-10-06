using Systems.GameSystem;

namespace Systems.UiSystem.Labels
{
    public class GoldLabel : StatLabel
    {
        protected override string GetValue()
        {
            return GameManager.Instance.Player.Gold  + "";
        }
    }
}