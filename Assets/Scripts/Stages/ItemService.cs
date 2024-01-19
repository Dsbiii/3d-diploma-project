using Assets.Scripts.Stages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemService : MonoBehaviour
{
    [SerializeField] private Item[] _items;
    public IReadOnlyCollection<Item> Items => _items;

    private void Awake()
    {
        foreach(var item in _items)
        {
            if(item.TryGetComponent(out DatedItemService datedItemService))
                datedItemService.DisplayDate();
            if (item.TryGetComponent(out DeffectedItemService deffectedItemService))
                deffectedItemService.DisplayDeffectsWithRandomValue();
        }
    }

}
