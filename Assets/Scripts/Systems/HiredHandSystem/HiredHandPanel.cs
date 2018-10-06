using System.Collections.Generic;
using System.Linq;
using Systems.GameSystem;
using Systems.ItemSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Systems.HiredHandSystem
{
    public class HiredHandPanel : MonoBehaviour
    {
        [SerializeField] private HiredHandMerchant merchant;
        [SerializeField] private GameObject buttonContainer;

        private Dictionary<Item, HiredHandButton> itemButtons = new Dictionary<Item, HiredHandButton>();
        private Tower currentSelectedTower => GameManager.Instance.TowerSelectionManager.CurrentSelectedTower;
        
        public void Update()
        {
            var items = merchant.ItemInventory.Items;
            var itemsToAdd = new List<Item>();
            var itemsToRemove = new List<Item>();

            items.ForEach(item =>
            {
                if (!itemButtons.ContainsKey(item))
                {
                    itemsToAdd.Add(item);
                }
            });
            
            itemButtons.Keys.ToList().ForEach(item =>
            {
                if (!items.Contains(item))
                {
                    itemsToRemove.Add(item);
                }
            });

            itemsToAdd.ForEach(AddButton);
            itemsToRemove.ForEach(RemoveButton);
        }

        public void AddButton(Item item)
        {
            HiredHandButton button = Instantiate(Resources.Load<HiredHandButton>("Prefabs/UI/HiredHandButton"));

            button.InitButton(item as HiredHandItem);
            button.transform.parent = buttonContainer.transform;
            
            itemButtons.Add(item, button);
        }

        public void RemoveButton(Item item)
        {
            var button = itemButtons[item];
            itemButtons.Remove(item);

            Destroy(button.gameObject);
        }
    }
}