using Systems.GameSystem;

namespace Systems.UiSystem.Labels
{
    public class AmbassadorLabel : StatLabel
    {
        protected override string GetValue()
        {
            return GameManager.Instance.Player.GetAmbassadors() + "";
        }
    }
}