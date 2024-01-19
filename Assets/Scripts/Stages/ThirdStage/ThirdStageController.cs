using Assets.Scripts;
using Assets.Scripts.Instruments;
using Assets.Scripts.Stages;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.SecondStage.Dismantling.Install_screw;
using Assets.Scripts.Stages.ThirdStage;
using Assets.Scripts.Stages.ThirdStage.Objects.Counter;
using Assets.Scripts.Stages.ThirdStage.Phases;
using Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase;
using Assets.Scripts.Stages.ThirdStage.Phases.CounterPhase;
using Assets.Scripts.Stages.ThirdStage.Phases.IKKPhase;
using Assets.Scripts.Stages.ThirdStage.Phases.TransformerPhase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdStageController : MonoBehaviour
{
    [SerializeField] private List<MarkerPoint> _markerPoints;
    [SerializeField] private List<Cable> _cablesCounter;
    [SerializeField] private MarkerSelector _markerSelector;
    [SerializeField] private LayerMask _transformatorLayer;
    [SerializeField] private List<GameObject> _objectsToOnOnStage;
    [SerializeField] private List<GameObject> _objectsToOffOnStage;
    [SerializeField] private Item _counter;
    [SerializeField] private Transformer _transformer;
    [SerializeField] private IKKTS _iKKTS;
    [SerializeField] private Item _cables;

    private ThirdStageModel _thirdStageModel;
    private MoveObjectService _moveObjectService;
    private GameState _gameState;
    private FirstStageItemPicker _itemPicker;
    private CountersPanel _countersPanel;
    private SelectingTransformatorPanel _selectingTransformatorPanel;
    private Inventory _inventory;
    private ItemTransferService _transformatorTransfer;
    private TransfromerPhase _transfromerPhase;
    private IKKPhase _iKKPhase;
    private CablesSelectingPanel _cablesSelectingPanel;

    private bool _isCanSetMarkers;

    public void Init(ThirdStageModel thirdStageModel,MoveObjectService moveObjectService, GameState gameState, FirstStageItemPicker firstStageItemPicker,
        CountersPanel countersPanel, SelectingTransformatorPanel selectingTransformatorPanel,
        Inventory inventory, ItemTransferService itemTransferService, TransfromerPhase transfromerPhase,
        IKKPhase iKKPhase,
        CablesSelectingPanel cablesSelectingPanel,
        InventoryView inventoryView)
    {
        _thirdStageModel = thirdStageModel;
        _moveObjectService = moveObjectService;
        _gameState = gameState;
        _itemPicker = firstStageItemPicker;
        _countersPanel = countersPanel;
        _selectingTransformatorPanel = selectingTransformatorPanel;
        _inventory = inventory;
        _transformatorTransfer = itemTransferService;
        _transfromerPhase = transfromerPhase;
        _cablesSelectingPanel = cablesSelectingPanel;
        _iKKPhase = iKKPhase;

    }

    public void ActiveMarkers()
    {
        _thirdStageModel.Markered();
        foreach(var item in _markerPoints)
        {
            item.SetupCap();
        }
    }

    public void PrepareStage()
    {
        foreach(var item in _objectsToOffOnStage)
            item.SetActive(false);
        foreach (var item in _objectsToOnOnStage)
            item.SetActive(true);
        foreach (var item in _cablesCounter)
            item.Off();
    }

    private void PickTransformator()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _transformatorLayer))
        {
            if (hit.transform != null && hit.transform.TryGetComponent(out Transformer transformer))
            {
                if(!transformer.IsPlant)
                {
                    _transfromerPhase.MoveTransformer();
                    _transformatorTransfer.SetTransformer(transformer, _transfromerPhase, true);
                }
            }
        }
    }

    public void Update()
    {
        _moveObjectService.MoveOnPlane();
        if (Input.GetMouseButtonDown(0))
        {
            PickTransformator();
        }

    }
}
