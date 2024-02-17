using Assets.Scripts.Instruments;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.FourthStage;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Stages.SecondStage.Panels
{
    public class ItemPreviewPanel : Panel
    {
        [SerializeField] private Button _replaceButton;
        [SerializeField] private Button _dressButton;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _close;

        [Inject] private FourthStageExamSystem _fourthStageExamSystem;
        [Inject] private FourthStageModel _fourthStageModel;

        private ParmaService _parmaService;
        private CE602MService _cE602MService;
        private Inventory _inventory;
        private InventoryPanel _inventoryPanel;
        private ItemPreview _firstStageItemPreview;
        private GameState _gameState;
        private SecondStageController _secondStageController;
        private bool _isCheckedDielecticAction;

        private void Awake()
        {
            _close.onClick.AddListener(Close);
            _replaceButton.onClick.AddListener(Replace);
            _dressButton.onClick.AddListener(DressUpItem);
        }

        public void Init(SecondStageController secondStageController,Inventory inventory,GameState gameState,InventoryPanel inventoryPanel, ItemPreview firstStageItemPreview)
        {
            _secondStageController = secondStageController;
            _inventory = inventory;
            _gameState = gameState;
            _inventoryPanel = inventoryPanel;
            _firstStageItemPreview = firstStageItemPreview;
        }

        public void InitFromController(ParmaService parmaService, CE602MService cE602MService)
        {
            _parmaService = parmaService;
            _cE602MService = cE602MService;
        }

        public void CloseInventoryPanel()
        {
            _inventoryPanel.Close();
        }

        public void Replace()
        {
            _firstStageItemPreview.SelectedItem.Refresh();
        }

        public void DressUpItem()
        {
            if (_inventory.DressUpItems.Select(item => item.ItemType).Contains(_firstStageItemPreview.SelectedItem.ItemType))
                return;

            bool isStop = false;

            foreach(var item in _inventory.DressUpItems)
            {
                if (item.ItemType == _firstStageItemPreview.SelectedItem.ItemType)
                {
                    isStop = true;
                    return;
                }
            }

            if (isStop)
                return;

            if (_fourthStageModel.IsExitedFromTP)
            {
                _fourthStageExamSystem.SetRightExitFromTP(false);
            }

            if (_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Helmets &&
                !_firstStageItemPreview.SelectedItem.IsDeffected && !_firstStageItemPreview.SelectedItem.IsOverdue)
            {
                    _fourthStageExamSystem.TakedRightPickedHelmetInspectionStage();
                _fourthStageModel.TakedPickedHelmetInspectionStage();
            }

            if (_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Cotton_Gloves &&
                !_firstStageItemPreview.SelectedItem.IsDeffected && !_firstStageItemPreview.SelectedItem.IsOverdue)
            {
                    _fourthStageExamSystem.TakedRightPickedCottonGlovesInspectionStage();
                _fourthStageModel.TakedPickedCottonGlovesInspectionStage();
            }
            if (_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Safety_Glasses &&
                !_firstStageItemPreview.SelectedItem.IsDeffected && !_firstStageItemPreview.SelectedItem.IsOverdue)
            {
                _fourthStageExamSystem.TakedRightPickedGlassesInspectionStage();
                _fourthStageModel.TakedPickedGlassesInspectionStage();
            }
            //if (_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Dielectric_Gloves)
            //{
            //    _fourthStageExamSystem.TakedRightPickedDielectricGlovesInspectionStage();
            //}

            if (_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Caps)
            {
                _secondStageController.TakedCups();
            }

            _inventory.DressItem(_firstStageItemPreview.SelectedItem);
            Close();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(2) && _firstStageItemPreview.SelectedItem != null)
            {
                _firstStageItemPreview.SelectedItem.ActionInPreview();
                if(_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Dielectric_Gloves)
                {
                    if (!_isCheckedDielecticAction)
                    {
                        _isCheckedDielecticAction = true;
                        if (_secondStageController.IsTakedParmaOrCE)
                            _secondStageController.AddNecessarilyAction("осмотреть и одеть х/б перчатки- до открытия дверей ТП;", 1, 0);
                        else
                            _secondStageController.AddNecessarilyAction("- осмотреть, проверить диэлектрические перчатки – до взятия прибора измерения (Пармы, клещей, СЕ);", 1, 0);
                    }
                }
            }
        }

        public override void Open()
        {
            _gameState.EnterInPreviewState();
            _inventoryPanel.Close();
            if(_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Parma_VAF ||
                _firstStageItemPreview.SelectedItem.ItemType == ItemTypes.CE602M ||
                _firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Clamp_Meters||
                _firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Plakat)
            {
                _replaceButton.gameObject.SetActive(false);
            }
            else
            {
                _replaceButton.gameObject.SetActive(true);
            }
            if (_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Helmets||
                _firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Cotton_Gloves||
                _firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Dielectric_Gloves ||
                _firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Safety_Glasses)
            {
                _dressButton.gameObject.SetActive(true);
            }
            else
            {
                _dressButton.gameObject.SetActive(false);
            }
            _panel.SetActive(true);
            base.Open();
        }

        public override void Close()
        {
            if (_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Caps)
            {
                _secondStageController.PutCaps();
            }
            _inventoryPanel.Open();
            _secondStageController.CloseItemPreview();
            _firstStageItemPreview.BackFromPreview(true);
            _panel.SetActive(false);
        }
    }
}