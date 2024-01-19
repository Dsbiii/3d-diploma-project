using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.ThirdStage;
using Assets.Scripts.Stages.ThirdStage.Panels;
using Assets.Scripts.Stages.ThirdStage.Phases;
using Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase;
using Assets.Scripts.Stages.ThirdStage.Phases.CounterPhase;
using Assets.Scripts.Stages.ThirdStage.Phases.IKKPhase;
using Assets.Scripts.Stages.ThirdStage.Phases.TransformerPhase;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.CompositeRoot
{
    public class ThirdStageCompositeRoot : CompositeRoot
    {
        [SerializeField] private ThirdStageModel _thirdStageModel;
        [SerializeField] private SetupPanel _setupPanel;
        [SerializeField] private CablesConnector _cablesConnector;
        [SerializeField] private FreeFlyCamera _freeFlyCamera;
        [SerializeField] private ThirdStagePanel _thirdStagePanel;
        [SerializeField] private InventoryPanel _inventoryPanel;
        [SerializeField] private ThirdStageController _thirdStageController;
        [SerializeField] private IKKSetupPanel _iKKSetupPanel;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private PlantObjectPanel _plantObjectPanel;
        [SerializeField] private ItemPreview _firstStageItemPreview;
        [SerializeField] private MoveObjectService _moveObjectService;
        [SerializeField] private GameState _gameState;
        [SerializeField] private FirstStageItemPicker _itemPicker;
        [SerializeField] private CountersPanel _countersPanel;
        [SerializeField] private SelectingTransformatorPanel _selectingTransformatorPanel;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private ItemTransferService _transformatorTransfer;
        [SerializeField] private TransfromerPhase _transfromerPhase;
        [SerializeField] private IKKPhase _iKKPhase;
        [SerializeField] private CablesSelectingPanel _cablesSelectingPanel;
        [SerializeField] private CablesPhase _cablesPhase;

        public override void Composite()
        {
            _setupPanel.Init(_iKKPhase, _transformatorTransfer,_thirdStageController,_countersPanel, _selectingTransformatorPanel, _cablesSelectingPanel);
            _cablesPhase.Init(_cablesConnector);
            _freeFlyCamera.Init(_gameState);
            _firstStageItemPreview.Init(_gameState);
            _inventoryPanel.Init(_gameState, _thirdStagePanel);
            _thirdStagePanel.Init(_inventoryPanel, _setupPanel);
            _moveObjectService.Init(_plantObjectPanel,_thirdStageModel);
            _itemPicker.Init(_firstStageItemPreview);
            _countersPanel.Init(_thirdStageModel, _gameState, _plantObjectPanel);
            _selectingTransformatorPanel.Init(_inventory, _gameState);
            _inventory.Init(_inventoryView);
            _iKKPhase.Init(_cablesConnector,_inventory, _iKKSetupPanel);
            _cablesSelectingPanel.Init(_cablesPhase);
            _iKKSetupPanel.Init(_iKKPhase, _thirdStageModel);
            _plantObjectPanel.Init(_cablesConnector,_gameState, _thirdStageModel);
            _thirdStageController.Init(_thirdStageModel,_moveObjectService, _gameState, _itemPicker, 
                _countersPanel, _selectingTransformatorPanel, _inventory, 
                _transformatorTransfer, _transfromerPhase, 
                _iKKPhase, _cablesSelectingPanel, _inventoryView);
        }
    }
}