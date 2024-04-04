using Assets.Scripts.Stages;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private InventoryItem _inventoryItemPrefab;
        [Inject] private GameMode _gameMode;

        private List<InventoryItem> _inventoryItems = new List<InventoryItem>();
        private IInventoryItemView _inventoryView;
        private List<Item> _dresUpItemsSorted = new List<Item>();
        private List<Item> _dressUpItems = new List<Item>();
        private SelectedInventoryItemsObject _selectedInventoryItemsObject;

        public ICollection<Item> DressUpItems => _dressUpItems;
        public IReadOnlyList<Item> Items => _inventoryItems.Select(item => item.Item).ToArray();

        private void Awake()
        {
            _selectedInventoryItemsObject = FindObjectOfType<SelectedInventoryItemsObject>();
        }

        public void Init(IInventoryItemView inventoryView)
        {
            _inventoryView = inventoryView;
        }

        public void DressItem(Item item)
        {
            item.DressItem();
            if(item.ItemType == ItemTypes.Dielectric_Gloves)
            {
                _dresUpItemsSorted.Add(item);
            }
            _dressUpItems.Add(item);
            RemoveItem(item);
        }

        public void AddObligatoryItems()
        {
            foreach(var item in _selectedInventoryItemsObject.ItemsObligatory)
            {
                if (!_inventoryItems.Select(x => x.Item).Contains(item))
                {
                    AddItem(item);
                }
            }
        }

        public void BackSortedItemsToInventory()
        {
            foreach (var item in _dresUpItemsSorted)
            {
                _dressUpItems.Remove(item);
                AddItem(item);
            }
            _dresUpItemsSorted.Clear();
        }

        public void BackAllItemsToInventory()
        {
            foreach (var item in _dressUpItems)
                AddItem(item);
            _dressUpItems.Clear();
        }

        public void BackThirdStageItemsToInventory()
        {
            foreach (var item in _dressUpItems)
            {
                if(item.ItemType == ItemTypes.Dielectric_Gloves)
                {
                    Debug.Log(item.ItemType);
                    AddItem(item);
                }
            }
            _dressUpItems.Clear();
        }

        public void AddItem(Item item)
        {
            if (_gameMode.IsDemo)
            {
                if (item.isDemo)
                {
                    item.TakeInInventory();
                    item.gameObject.SetActive(false);
                    var inventoryItem = Instantiate(_inventoryItemPrefab);
                    inventoryItem.SetItem(item);
                    _inventoryItems.Add(inventoryItem);
                    _inventoryView.DisplayInventoryItem(inventoryItem);
                }
            }
            else
            {
                item.TakeInInventory();
                item.gameObject.SetActive(false);
                var inventoryItem = Instantiate(_inventoryItemPrefab);
                inventoryItem.SetItem(item);
                _inventoryItems.Add(inventoryItem);
                _inventoryView.DisplayInventoryItem(inventoryItem);
            }
        }

        public void RemoveItem(Item item)
        {
            var inventoryItem = _inventoryItems.FirstOrDefault(x => x.Item == item);
            if(inventoryItem  != null)
            {
                item.ResetAction();
                _inventoryItems.Remove(inventoryItem);
                Destroy(inventoryItem.gameObject);
            }
        }


    }
}