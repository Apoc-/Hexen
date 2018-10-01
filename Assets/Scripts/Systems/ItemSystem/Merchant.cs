using System.Collections.Generic;
using Assets.Scripts.Definitions.HiredHands;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.HandSystem;
using UnityEditorInternal;
using UnityEngine;

namespace Assets.Scripts.Systems.TowerSystem
{
    public abstract class Merchant : MonoBehaviour
    {
        private Dictionary<Rarities, List<Item>> registeredItems = new Dictionary<Rarities, List<Item>>
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

            registeredItems[item.Rarity].Add(item);
        }

        public List<Item> GetRegisteredItemsOfRarity(Rarities rarity)
        {
            return registeredItems[rarity];
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

            if (ItemInventory.MoveItem(item, inventory))
            {
                player.DecreaseGold(item.Cost);
                return true;
            };

            return false;
        }
    }
}