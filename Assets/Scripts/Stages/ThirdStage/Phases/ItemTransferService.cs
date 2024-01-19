using Assets.Scripts.Stages.ThirdStage.Objects.Counter;
using Assets.Scripts.Stages.ThirdStage.Phases.IKKPhase;
using Assets.Scripts.Stages.ThirdStage.Phases.TransformerPhase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.ThirdStage.Phases
{
    public class ItemTransferService : MonoBehaviour
    {
        [SerializeField] private Transform _ikkPoint;
        [SerializeField] private IKKSetupPanel _iKKSetupPanel;
        [SerializeField] private LayerMask _planeLayerMask;
        private Item _currentItem;
        private ITransferHandler _transferHandler;
        private bool _isTransformator;

        public bool IsPlanted { get; private set; }

        public void SetTransformer(Item transformer, ITransferHandler transferHandler, bool isTransformator = false)
        {
            _isTransformator = isTransformator;
            _transferHandler = transferHandler;
            _currentItem = transformer;
            _currentItem.transform.position = _ikkPoint.transform.position;
            _currentItem.gameObject.SetActive(true);
            if(!_isTransformator)
                _iKKSetupPanel.DisplayPoints();
        }

        public void Plant()
        {
            IsPlanted = true;
        }

        public void UndoCurrentItem()
        {
            _currentItem = null;
        }
        private bool TryPickUI()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            foreach (var item in results)
                if (item.gameObject.tag == "UI")
                    return true;
            return false;
        }



        private void Update()
        {
            if (_currentItem != null)
            {
                if (_isTransformator)
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _planeLayerMask))
                    {
                        Vector3 objectPosition = hit.point;
                        _currentItem.transform.position = objectPosition;
                    }

                    if (_transferHandler.IsPlaced())
                    {
                        _transferHandler.EndTransferItem(_currentItem);
                    }

                    //if (Input.GetMouseButtonUp(0))
                    //    _transferHandler.EndTransferItem(_currentItem);
                }
                else
                {
                    if (!TryPickUI() && Input.GetMouseButton(0))
                    {
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _planeLayerMask))
                        {
                            Vector3 objectPosition = hit.point;
                            _currentItem.transform.position = objectPosition -= new Vector3(0, 0, 0);
                        }
                        _transferHandler.EndTransferItem(_currentItem);
                    }
                }
            }
        }

    }
}