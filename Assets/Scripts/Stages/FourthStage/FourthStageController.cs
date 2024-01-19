using Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector;
using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.FourthStage.CablesSystem;
using Assets.Scripts.Stages.FourthStage.SelectingCablesPanel;
using Assets.Scripts.Stages.SecondStage.Panels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class FourthStageController : MonoBehaviour
    {
        [SerializeField] private GameObject _tkView;
        [SerializeField] private GameObject[] _objectsToOff;
        [SerializeField] private GameObject[] _objectsToOn;
        [SerializeField] private SecondStagePanel _secondStagePanel;
        [SerializeField] private MovableObject _uspdBlockMovableObject;
        [SerializeField] private MovableObject _powerBlockMovableObject;

        [Inject] private Inventory _inventory;
        [Inject] private FourthStageModel _fourthStageModel;
        [Inject] private MovableObjectService _movableObjectService;
        [Inject] private GameState _gameState;
        [Inject] private ItemPreviewPanel _itemPreviewPanel;
        [Inject] private ItemPreview _itemPreview;
        [Inject] private FourthStageCableConnector _fourthStageCableConnector;
        [Inject] private FourthStageCablesSelectingPanel _fourthStageCablesSelectingPanel;
        [Inject] private AntenaSevice _antenaSevice;
        [Inject] private SIMService _simService;
        [Inject] private FourthStagePlombService _fourthStagePlombService;
        [Inject] private LaptopCableService _laptopCableService;
        [Inject] private CounterCableService _counterCableService;

        private void Start()
        {
            if (PlayerPrefs.HasKey("USDPStage"))
            {
                InitStage();
                PlayerPrefs.DeleteKey("USDPStage");
            }
            if (PlayerPrefs.HasKey("USDPFifthStage"))
            {
                _secondStagePanel.EndThirdStageWithOutEnterFourthStage();
                _secondStagePanel.EndFourthStage();
                _secondStagePanel.EndFourthStageEnterInFifth();
                _tkView.SetActive(false);
                PlayerPrefs.DeleteKey("USDPFifthStage");
            }
            if (PlayerPrefs.HasKey("USDPSixStage"))
            {
                _secondStagePanel.StartFifthStage();
                _tkView.SetActive(false);
                PlayerPrefs.DeleteKey("USDPSixStage");
            }
        }

        public void InitStage()
        {
            foreach(var item in _fourthStageModel.Items)
            {
                if (item.TryGetComponent(out DatedItemService datedItemService))
                    datedItemService.DisplayDate();
                if (item.TryGetComponent(out DeffectedItemService deffectedItemService))
                    deffectedItemService.DisplayDeffectsWithRandomValue();

                _inventory.AddItem(item);
            }
            _secondStagePanel.OpenDoors();
            _secondStagePanel.EndSetup();
            foreach (var item in _objectsToOff)
            {
                item.SetActive(false);
            }
            foreach (var item in _objectsToOn)
            {
                item.SetActive(true);
            }
            _tkView.SetActive(true);
        }

        public void ExitFromStage()
        {
            foreach (var item in _fourthStageModel.Items)
            {
                if(_fourthStageModel.ItemsToRemove.Contains(item))
                    _inventory.RemoveItem(item);
            }
        }

        public void EndTKView()
        {
            _tkView.SetActive(false);
            _gameState.EnterInDismantling();
        }

        private void Update()
        {
            _movableObjectService.MoveOnPlane();
            HandlePickInventoryItem();
        }

        public void DisplayCablesConnectPoints()
        {
            _fourthStageCableConnector.DisplayIndicatorsCables();
        }

        private void HandlePickInventoryItem()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (TryPickInventoryItem(out InventoryItem inventoryItem))
                {
                    _fourthStageModel.SelectItem(inventoryItem.Item);
                    if (inventoryItem.Item.ItemType == ItemTypes.USPDBlock && !_uspdBlockMovableObject.IsPlanted)
                    {
                        _uspdBlockMovableObject.gameObject.SetActive(true);
                        _fourthStageModel.SetSelectedMovableObject(_uspdBlockMovableObject);
                    }
                    else if (inventoryItem.Item.ItemType == ItemTypes.PowerBlock && !_powerBlockMovableObject.IsPlanted)
                    {
                        _powerBlockMovableObject.gameObject.SetActive(true);
                        _fourthStageModel.SetSelectedMovableObject(_powerBlockMovableObject);
                    }
                    else if (inventoryItem.Item.ItemType == ItemTypes.CableForCounter)
                    {
                        _counterCableService.Select(inventoryItem.Item);
                    }
                    else if (inventoryItem.Item.ItemType == ItemTypes.CableForLaptop)
                    {
                        _laptopCableService.Select(inventoryItem.Item);
                    }
                    else if (inventoryItem.Item.ItemType == ItemTypes.Cables)
                    {
                        _fourthStageCablesSelectingPanel.Open();
                    }
                    else if (inventoryItem.Item.ItemType == ItemTypes.Antenna)
                    {
                        _antenaSevice.Select(inventoryItem.Item);
                    }
                    else if(inventoryItem.Item.ItemType == ItemTypes.SIM_1 ||
                        inventoryItem.Item.ItemType == ItemTypes.SIM_2)
                    {
                        _simService.Select(inventoryItem.Item);
                    }else if(inventoryItem.Item.ItemType == ItemTypes.Plombs)
                    {
                        _fourthStagePlombService.Select(inventoryItem.Item);
                    }
                    else if(inventoryItem.Item.ItemType == ItemTypes.USPDBlock||
                        inventoryItem.Item.ItemType == ItemTypes.PowerBlock ||
                        inventoryItem.Item.ItemType == ItemTypes.CableForCounter ||
                        inventoryItem.Item.ItemType == ItemTypes.CableForLaptop ||
                        inventoryItem.Item.ItemType == ItemTypes.Cables ||
                        inventoryItem.Item.ItemType == ItemTypes.Antenna ||
                        inventoryItem.Item.ItemType == ItemTypes.SIM_1 ||
                        inventoryItem.Item.ItemType == ItemTypes.SIM_2 ||
                        inventoryItem.Item.ItemType == ItemTypes.Plombs
                        )
                    {
                        _itemPreview.PreviewItem(inventoryItem.Item);
                        _gameState.EnterInPreviewState();
                        _itemPreviewPanel.Open();
                    }
                }
            }
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