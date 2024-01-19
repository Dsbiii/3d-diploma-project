using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage
{
    public class SecondStageView : MonoBehaviour, IInventoryItemView
    {
        [SerializeField] private Transform _parent;
        private Inventory _inventory;

        public void DisplayInventoryItem(InventoryItem inventoryItem)
        {
            inventoryItem.transform.SetParent(_parent);
            inventoryItem.transform.localScale = new Vector3(1, 1, 1);
        }

        public void Init(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void InitInventory(IEnumerable<Item> items)
        {
            foreach(var item in items)
            {
                _inventory.AddItem(item);
            }
        }

    }
}