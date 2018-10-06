using System.Collections.Generic;
using System.Linq;
using Systems.GameSystem;
using Systems.ItemSystem;
using Systems.TowerSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems.HiredHandSystem
{
    public class HiredHandPanel : MonoBehaviour
    {
        [FormerlySerializedAs("merchant")] [SerializeField] private HiredHandMerchant _merchant;
        [FormerlySerializedAs("buttonContainer")] [SerializeField] private GameObject _buttonContainer;

        private readonly Dictionary<Item, HiredHandButton> _itemButtons = new Dictionary<Item, HiredHandButton>();
        private Tower CurrentSelectedTower => GameManager.Instance.TowerSelectionManager.CurrentSelectedTower;
        
        public void Update()
        {
            var items = _merchant.ItemInventory.Items;
            var itemsToAdd = new List<Item>();
            var itemsToRemove = new List<Item>();

            items.ForEach(item =>
            {
                if (!_itemButtons.ContainsKey(item))
                {
                    itemsToAdd.Add(item);
                }
            });
            
            _itemButtons.Keys.ToList().ForEach(item =>
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
            button.transform.parent = _buttonContainer.transform;
            
            _itemButtons.Add(item, button);
        }

        public void RemoveButton(Item item)
        {
            var button = _itemButtons[item];
            _itemButtons.Remove(item);

            Destroy(button.gameObject);
        }
    }
}