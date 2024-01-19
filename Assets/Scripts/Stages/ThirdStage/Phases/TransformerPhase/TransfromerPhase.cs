using System.Collections;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Stages.ThirdStage.Objects.Counter;
using System.Collections.Generic;

namespace Assets.Scripts.Stages.ThirdStage.Phases.TransformerPhase
{
    public class TransfromerPhase : MonoBehaviour, ITransferHandler
    {
        [SerializeField] private TransformerSetupPanel _transformerSetupPanel;
        [SerializeField] private ItemTransferService _itemTransferService;
        [SerializeField] private GameObject[] _onn;
        [SerializeField] private GameObject _transformersItems;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private ItemPlacePoint[] _transformatorPlaces;
        private List<Item> _itemsTransformator = new List<Item>();
        private ItemPlacePoint _currentItemPlacePoint;
        private Transformer _currentTransformer;
        private ThirdStageModel _thirdStage;

        private void Awake()
        {
            _thirdStage = FindObjectOfType<ThirdStageModel>();
        }

        public void MoveTransformer()
        {
            foreach (var place in _transformatorPlaces)
                place.TryDisplayPoint();
        }

        public void Undo()
        {
            _currentTransformer.UnPlant();
            _currentItemPlacePoint.UndoTemorartyItem();
            _itemTransferService.UndoCurrentItem();
            _currentTransformer.ResetPosition();
            _currentTransformer = null;
            _currentItemPlacePoint = null;
        }

        public void Setup()
        {
            _currentItemPlacePoint.SetItem(_currentTransformer);
            _itemTransferService.UndoCurrentItem();
            _currentTransformer = null;
            _currentItemPlacePoint = null;
        }

        private void Update()
        {
            if (_currentTransformer == null)
                return;
            float mw = Input.GetAxis("Mouse ScrollWheel");
            if (mw > 0.1)
            {
                _currentTransformer.transform.Rotate(90, 0, 0);
            }
            if (mw < -0.1)
            {
                _currentTransformer.transform.Rotate(-90, 0, 0);
            }
        }

        public void EndTransferItem(Item item)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                if (hit.transform.TryGetComponent(out ItemPlacePoint transformatorPlace))
                {
                    if (!transformatorPlace.IsPlanted)
                    {
                        _itemsTransformator.Add(item);
                        if (_transformatorPlaces.Where(item => item.IsPlanted).ToArray().Length >= _transformatorPlaces.Length - 1)
                        {
                            _transformersItems.SetActive(true);
                            foreach (var x in _onn)
                                x.SetActive(true);
                            //foreach (var transformator in _itemsTransformator)
                            //    transformator.gameObject.SetActive(false);
                            _itemsTransformator.Clear();
                        }

                        if (item is Transformer transformer)
                            _currentTransformer = transformer;
                        _currentTransformer.Plant();
                        _transformerSetupPanel.Open();
                        _currentItemPlacePoint = transformatorPlace;
                        _currentItemPlacePoint.TemporaryItem(item);
                        _itemTransferService.UndoCurrentItem();
                    }
                    else
                    {
                        if (item is Transformer transformer)
                        {
                            _itemTransferService.UndoCurrentItem();
                            transformer.ResetPosition();
                        }
                    }
                }
                else
                {
                    if (item is Transformer transformer)
                    {
                        _itemTransferService.UndoCurrentItem();
                        transformer.ResetPosition();
                    }
                }
            }
            else
            {
                if (item is Transformer transformer)
                {
                    _itemTransferService.UndoCurrentItem();

                    transformer.ResetPosition();
                }
            }
            foreach (var place in _transformatorPlaces)
                place.OffPoint();
        }

        public bool IsPlaced()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                if (hit.transform.TryGetComponent(out ItemPlacePoint transformatorPlace))
                {
                    if (!transformatorPlace.IsPlanted)
                        return true;
                }
            }
            return false;
        }
    }
}