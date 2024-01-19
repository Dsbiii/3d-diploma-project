using Assets.Scripts.Stages;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;



[CreateAssetMenu(fileName = "ItemsInventory", menuName = "ScriptableObject/ItemsInventory")]
public class ItemsInventory : ScriptableObject
{
    [SerializeField] private Item[] _items;

    public IReadOnlyCollection<Item> Items => _items;

    //public void SetItems(Item[] items)
    //{
    //    _items = items;
    //    EditorUtility.SetDirty(this);
    //}

}