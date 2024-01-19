using System;
using UnityEngine;

namespace Assets.Scripts.Stages.FirstStage
{
    public class FirstStageModel
    {
        public event Action<Item> OnSelectedItem;
        public event Action<Item> OnSelectedInventoryItem;

        public Item CurrentItem { get; private set; }
        

        public void SetCurrentItem(Item item)
        {
            CurrentItem = item;
            OnSelectedItem?.Invoke(item);
        }

        public void SetCurrentInventoryItem(InventoryItem inventoryItem)
        {
            CurrentItem = inventoryItem.Item;
            OnSelectedInventoryItem?.Invoke(CurrentItem);
        }

    }
}