using Systems.ItemSystem;
using Definitions.HiredHands;

namespace Systems.HiredHandSystem
{
    public class HiredHandMerchant : Merchant
    {
        protected override void RegisterItems()
        {
            RegisterItem<Worker>();
        }
    }
}