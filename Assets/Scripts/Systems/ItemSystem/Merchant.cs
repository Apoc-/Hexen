using System.Collections.Generic;
using Systems.GameSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Systems.ItemSystem
{
    public abstract class Merchant : MonoBehaviour
    {
        private Dictionary<Rarities, List<Item>> _registeredItems = new Dictionary<Rarities, List<Item>>
        {
            { Rarities.Common, new List<Item>() },
            { Rarities.Uncommon, new List<Item>() },
            { Rarities.Rare, new List<Item>() },
            { Rarities.Legendary, new List<Item>() }
        };

        public Inventory ItemInventory;
        public GameObject RegisteredItemsContainer;

        public void Awake()
        {
            RegisterItems();
            ItemInventory.InitInventory(8);
        }

        protected abstract void RegisterItems();

        protected void RegisterItem<T>() where T : Item
        {
            GameObject go = new GameObject();
            Item item = go.AddComponent<T>();

            item.InitItem();

            go.name = item.Name;
            go.transform.parent = RegisteredItemsContainer.transform;
            go.SetActive(false);

            _registeredItems[item.Rarity].Add(item);
        }

        public List<Item> GetRegisteredItemsOfRarity(Rarities rarity)
        {
            return _registeredItems[rarity];
        }

        public void OfferItem(Item item)
        {
            var instance = Instantiate(item);
            instance.InitItem();
            instance.name = instance.Name;

            ItemInventory.AddItem(instance);
        }

        public bool BuyItem(Item item, Player player, Inventory inventory)
        {
            if (item.Cost > player.Gold) return false;

            if (!ItemInventory.MoveItem(item, inventory)) return false;
            
            player.DecreaseGold(item.Cost);
            return true;
        }
    }
}