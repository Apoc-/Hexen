using Systems.TowerSystem;
using UnityEngine;

namespace Systems.GameSystem
{
    public class DebugManager : MonoBehaviour
    {
        public void DebugAddAmbassador()
        {
            GameManager.Instance.Player.IncreaseAmbassadors(1);
        }

        public void DebugRefreshments()
        {
            GameManager.Instance.Player.IncreaseGold(1000);
            GameManager.Instance.Player.Lives = 1000;
        }

        public void AddItem()
        {
            var merchant = GameManager.Instance.HiredHandMerchant;
            var item = merchant.GetRegisteredItemsOfRarity(Rarities.Common)[0];

            merchant.OfferItem(item);
        }

        public void BuyItem()
        {
            var merchant = GameManager.Instance.HiredHandMerchant;
            var item = merchant.ItemInventory.Items[0];

            var tower = GameManager.Instance.TowerBuildManager.BuiltTowers[0];
            merchant.BuyItem(item, GameManager.Instance.Player, tower.Inventory);
        }
    }
}