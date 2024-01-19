using Assets.Scripts.Stages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class InventoryView : MonoBehaviour, IInventoryItemView
    {
        [SerializeField] private Transform _parent;

        public void DisplayInventoryItem(InventoryItem inventoryItem)
        {
            inventoryItem.transform.SetParent(_parent);
            inventoryItem.transform.localScale = new Vector3(1, 1, 1);
        }

    }
}