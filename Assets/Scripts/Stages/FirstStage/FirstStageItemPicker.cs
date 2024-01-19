using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.FirstStage
{
    public class FirstStageItemPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _screwdriverLayerMask;
        [SerializeField] private LayerMask _itemLayerMask;

        private ItemPreview _firstStageItemPreview;

        public void Init(ItemPreview firstStageItemPreview)
        {
            _firstStageItemPreview = firstStageItemPreview;
        }

        public bool TryPickItem(out Item item)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _itemLayerMask))
            {
                if (hit.transform != null && hit.transform.TryGetComponent(out item))
                {
                    return true;
                }
            }
            item = null;
            return false;
        }

        public bool TryPickScrewdriver(out Screwdriver screwdriver)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _screwdriverLayerMask))
            {
                if (hit.transform != null && hit.transform.TryGetComponent(out screwdriver))
                {
                    return true;
                }
            }
            screwdriver = null;
            return false;

        }

        public bool TryPickInventoryItem(out InventoryItem inventoryItem)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            foreach (var item in results)
            {
                if (item.gameObject.TryGetComponent(out inventoryItem))
                {
                    return true;
                }
            }
            inventoryItem = null;
            return false;

        }
    }
}