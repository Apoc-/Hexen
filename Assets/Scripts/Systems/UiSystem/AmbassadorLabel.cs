using Assets.Scripts.Systems.GameSystem;

namespace Assets.Scripts.Systems.UiSystem
{
    public class AmbassadorLabel : StatLabel
    {
        protected override string GetValue()
        {
            return GameManager.Instance.Player.GetAmbassadors() + "";
        }
    }
}