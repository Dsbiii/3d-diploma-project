using Assets.Scripts.Instruments;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.SecondStage;
using Assets.Scripts.Stages.SecondStage.Dismantling;
using Assets.Scripts.Stages.SecondStage.Electric_Box;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface.Windows;
using Assets.Scripts.Stages.SecondStage.Panels;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.CompositeRoot
{
    public class SecondStageCompositeRoot : CompositeRoot
    {
        [SerializeField] private VectorDiogram _vectorDiogram;
        [SerializeField] private PlombService _plombService;
        [SerializeField] private CapsService _capsService;
        [SerializeField] private CounterView _counterView;
        [SerializeField] private ElectricBoxView _electricBoxView;
        [SerializeField] private CE602MItemsPicker _cE602MItemsPicker;
        [SerializeField] private IKK _iKK;
        [SerializeField] private SecondStageState _secondStageState;
        [SerializeField] private WorkingModeWindow _workingModeWindow;
        [SerializeField] private NetworkSettingsWindow _networkSettingsWindow;
        [SerializeField] private MeasurementsWindow _measurementsWindow;
        [SerializeField] private ErrorWindow _errorWindow;
        [SerializeField] private AutomaticModeWindow _automaticModeWindow;
        [SerializeField] private CEView _cEView;
        [SerializeField] private CEkeyboard _cEkeyboard;
        [SerializeField] private DoorPicker _doorPicker;
        [SerializeField] private FreeFlyCamera _freeFlyCamera;
        [SerializeField] private SelectingActionsPanel _selectingActionsPanel;
        [SerializeField] private FirstStageItemPicker _firstStageItemPicker;
        [SerializeField] private SecondStageController _secondStageController;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private SecondStageView _secondStageView;
        [SerializeField] private ActionService _actionService;
        [SerializeField] private GameState _gameState;
        [SerializeField] private InventoryPanel _inventoryPanel;
        [SerializeField] private ItemPreviewPanel _itemPreviewPanel;
        [SerializeField] private SecondStagePanel _secondStagePanel;
        [SerializeField] private ItemPreview _firstStageItemPreview;

        private ActionPicker _actionPicker;
        private SecondStageModel _secondStageModel;

        public override void Composite()
        {
            _gameState.EnterInInspection();
            _workingModeWindow.Init(_measurementsWindow);
            _measurementsWindow.Init(_networkSettingsWindow, _errorWindow, _vectorDiogram);
            _errorWindow.Init(_automaticModeWindow);
            _freeFlyCamera.Init(_gameState);
            _cEkeyboard.Init(_cEView);
            _actionPicker = new ActionPicker();
            _secondStageModel = new SecondStageModel();
            _plombService.Init(_secondStageModel);
            _capsService.Init(_secondStageModel);
            _iKK.Init(_secondStageModel);
            _counterView.Init(_secondStageModel);
            _firstStageItemPreview.Init(_gameState);
            _secondStagePanel.Init(_inventory,_secondStageController,_secondStageModel,_secondStageState,_gameState, _inventoryPanel);
            _selectingActionsPanel.Init(_secondStagePanel);
            _firstStageItemPicker.Init(_firstStageItemPreview);
            _inventory.Init(_secondStageView);
            _secondStageView.Init(_inventory);
            _actionService.Init(_gameState, _actionPicker);
            _inventoryPanel.Init(_secondStagePanel, _gameState);
            _itemPreviewPanel.Init(_secondStageController,_inventory,_gameState,_inventoryPanel, _firstStageItemPreview);
            _secondStageController.Init(_plombService,_capsService,_counterView,_inventoryPanel,_cE602MItemsPicker,_iKK,_secondStageModel, _secondStagePanel, _doorPicker, _itemPreviewPanel, _firstStageItemPreview, _secondStageView, _gameState, _firstStageItemPicker, _secondStageState);
            _gameState.EnterInSelectingActions();
        }
    }
}