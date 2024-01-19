using Assets.Scripts.Stages;
using Assets.Scripts.Stages.FirstStage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedInventoryItemsObject : MonoBehaviour
{
    private List<Item> _items;
    private List<Item> _itemsToDeffect = new List<Item>();
    private List<Item> _itemsObligatory = new List<Item>();

    public IEnumerable<Item> ItemsObligatory => _itemsObligatory;
    public IReadOnlyCollection<Item> Items => _items;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            if(_items != null)
            {
                if (_items.Count > 0)
                {
                    foreach (var item in _items)
                    {
                        if (item.gameObject != null)
                            Destroy(item.gameObject);
                    }
                    _items.Clear();
                }
            }
            if (_itemsToDeffect != null)
            {
                if (_itemsToDeffect.Count > 0)
                {
                    foreach (var item in _itemsToDeffect)
                    {
                        if (item.gameObject != null)
                            Destroy(item.gameObject);
                    }
                    _itemsToDeffect.Clear();
                }
            }

            if(_itemsObligatory != null)
            {
                if (_itemsObligatory.Count > 0)
                {
                    foreach (var item in _itemsObligatory)
                    {
                        if (item.gameObject != null)
                            Destroy(item.gameObject);
                    }
                    _itemsObligatory.Clear();
                }
            }
        }
    }
    public void SetItems(IEnumerable<Item> items)
    {
        foreach (var item in items)
            Debug.Log("Item " + item);
        _items = new List<Item>(items);
        foreach(var item in _items)
        {
            if(item.ItemType == ItemTypes.Boots ||
                item.ItemType == ItemTypes.Dielectric_Gloves ||
                item.ItemType == ItemTypes.Helmets ||
                item.ItemType == ItemTypes.Safety_Glasses)
            {
                _itemsToDeffect.Add(item);
            }
            
        }
    }

    public void AddAllItemsToObligatory(IReadOnlyCollection<Item> items, Transform parent)
    {
        if (items.Count == 0)
            return;
        foreach (var item in items)
        {
            if (
                item.ItemType == ItemTypes.Dielectric_Gloves ||
                item.ItemType == ItemTypes.Helmets ||
                item.ItemType == ItemTypes.Safety_Glasses ||
                item.ItemType == ItemTypes.Cotton_Gloves)
            {
                item.Refresh(false);
                item.transform.SetParent(parent);
                _itemsObligatory.Add(item);
            }

        }
    }

    public void DeffectItemsToDeffect()
    {
        foreach (var item in _itemsToDeffect)
        {
            if(!item.IsDeffected)
                item.TryDeffectItem(); 
        }
    }
}
