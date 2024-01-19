using Assets.Scripts.Stages;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        public Item Item { get; private set; }


        public void SetItem(Item item)
        {
            Item = item;
            _icon.sprite = Item.Icon;
        }
    }
}