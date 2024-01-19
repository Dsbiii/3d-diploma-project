using Assets.Scripts;
using Assets.Scripts.Stages;
using Assets.Scripts.Stages.FirstStage;
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
using UnityEngine.UI;

namespace Assets.Scripts.Stages.ThirdStage.Panels
{
    public class SetupPanel : Panel
    {
        [SerializeField] private ThirdStageModel _thirdStageModel;
        [SerializeField] private IKKTS _ikkts;  
        [SerializeField] private GameObject _panel;

        [SerializeField] private Button _ikkButton;
        [SerializeField] private Button _markersButton;
        [SerializeField] private Button _counterButton;
        [SerializeField] private Button _transformatorButton;
        [SerializeField] private Button _cableButton;

        private IKKPhase _iKKPhase;
        private ItemTransferService _transformatorTransfer;
        private ThirdStageController _thirdStageController;
        private CountersPanel _countersPanel;
        private SelectingTransformatorPanel _selectingTransformatorPanel;
        private CablesSelectingPanel _cablesSelectingPanel;

        private void Awake()
        {
            _counterButton.onClick.AddListener(Counter);
            _transformatorButton.onClick.AddListener(Transformator);
            _cableButton.onClick.AddListener(Cable);
            _markersButton.onClick.AddListener(Markers);
            _ikkButton.onClick.AddListener(IKK);
        }

        public void Init(IKKPhase iKKPhase,ItemTransferService transformatorTransfer,ThirdStageController thirdStageController,CountersPanel countersPanel, SelectingTransformatorPanel selectingTransformatorPanel, CablesSelectingPanel cablesSelectingPanel)
        {
            _iKKPhase = iKKPhase;
            _transformatorTransfer = transformatorTransfer;
            _countersPanel = countersPanel;
            _selectingTransformatorPanel = selectingTransformatorPanel;
            _cablesSelectingPanel = cablesSelectingPanel;
            _thirdStageController = thirdStageController;
        }

        public void IKK()
        {
            _transformatorTransfer.SetTransformer(_ikkts, _iKKPhase);
            _ikkButton.gameObject.SetActive(false);
            TryToClose();
        }

        public void Markers()
        {
            _thirdStageController.ActiveMarkers();
            _markersButton.gameObject.SetActive(false);
            TryToClose();
        }

        public void Counter()
        {
            _countersPanel.Open();
            _counterButton.gameObject.SetActive(false);
            TryToClose();
        }

        public void Transformator()
        {
            _selectingTransformatorPanel.Open();
            _transformatorButton.gameObject.SetActive(false);
            TryToClose();
        }

        public bool CheckIsRightSetuped()
        {
            if (_thirdStageModel.IsPlantedCounter && _thirdStageModel.IsPlantedTransformers && _thirdStageModel.IsPlatedIKK)
            {
                Debug.Log("CheckIsRightSetuped True");
                return true;
            }
            Debug.Log("CheckIsRightSetuped False");
            return false;
        }
        public void Cable()
        {
            _cablesSelectingPanel.Open();
            _cableButton.gameObject.SetActive(false);
            TryToClose();
        }

        private void TryToClose()
        {
            if (!_ikkButton.gameObject.activeInHierarchy && !_markersButton.gameObject.activeSelf && !_counterButton.gameObject.activeSelf && !_transformatorButton.gameObject.activeSelf && !_cableButton.gameObject.activeSelf)
                Close();
        }

        public override void Open()
        {
            _panel.SetActive(true);
            base.Open();
        }
        public override void Close()
        {
            _panel.SetActive(false);
        }
    }
}