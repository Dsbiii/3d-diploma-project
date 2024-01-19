using Assets.Scripts.Stages.SecondStage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Instruments
{
    public class PlombService : MonoBehaviour
    {
        [SerializeField] private PlombPoint[] _plombs;
        [SerializeField] private SecondStageController _secondStageController;
        [SerializeField] private Texture2D _cursorTexture;
        [SerializeField] private LayerMask _layer;
        private SecondStageModel _secondStageModel;
        private bool _isActive;

        public void Init(SecondStageModel secondStageModel)
        {
            _secondStageModel = secondStageModel;
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
                    if (inventoryItem.Item.ItemType == ItemTypes.Plombs)
                        return true;
                }
            }
            inventoryItem = null;
            return false;

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(TryPickInventoryItem(out InventoryItem inventoryItem))
                {
                    foreach (var item in _plombs)
                        item.OnColliders();
                    _isActive = true;
                    Cursor.SetCursor(_cursorTexture, Vector2.zero, CursorMode.Auto);
                    _secondStageController.TakePlombsAndCheckScrewDriverAction();
                }
            }

            if(Input.GetMouseButtonUp(0) && _isActive)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                TrySetupPlomb();
                _isActive = false;
                foreach (var item in _plombs)
                    item.OffColliders();
            }
        }

        public void TrySetupPlomb()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
            {
                if (hit.transform.TryGetComponent(out PlombPoint capPoint))
                {
                    capPoint.SetupCap();
                }
            }
        }
    }
}