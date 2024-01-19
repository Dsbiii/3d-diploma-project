using Assets.Scripts.Instruments;
using Assets.Scripts.Stages.FifthStage;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.FourthStage;
using Assets.Scripts.Stages.FourthStage.CablesSystem;
using Assets.Scripts.Stages.SecondStage.Dismantling;
using Assets.Scripts.Stages.SecondStage.Electric_Box;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M;
using Assets.Scripts.Stages.ThirdStage;
using Assets.Scripts.Stages.ThirdStage.CablesSystem;
using Assets.Scripts.Stages.ThirdStage.Panels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Stages.SecondStage.Panels
{
    public class SecondStagePanel : Panel
    {
        [SerializeField] private PlombPoint _counterPlombPoint;
        [SerializeField] private PlombPoint _ikkPlombPoint;
        [SerializeField] private Button _startFifthStageButton;
        [SerializeField] private GameObject _openPyramidButton;
        [SerializeField] private GameObject[] _plakatToOnForuthStage;
        [SerializeField] private FourthStagePlomb[] _fourthStageScriptsPlombs;
        [SerializeField] private AntenaPoint _antenaPoint;
        [SerializeField] private AktReport _aktReport;
        [SerializeField] private GameObject _computerButton;
        [SerializeField] private List<GameObject> _shinaOn;
        [SerializeField] private List<GameObject> _shinaOff;
        [SerializeField] private List<GameObject> _cablesFourthStage;
        [SerializeField] private GameObject _tpOn;
        [SerializeField] private GameObject _powerIndicator;
        [SerializeField] private List<SIMPoint> _simPoints;
        [SerializeField] private MovableObject _powerSUP;
        [SerializeField] private MovableObject _controller;
        [SerializeField] private GameObject _endFourthStageBackGround;
        [SerializeField] private Door[] _doors;
        [SerializeField] private MeshRenderer[] _screwsMustToOff;
        [SerializeField] private MeshRenderer[] _screwsMustToOn;

        [SerializeField] private PlakatService _plakatService;
        [SerializeField] private GameObject _offTPPanel;
        [SerializeField] private GameObject _ikkPoints;
        [SerializeField] private Text _score;
        [SerializeField] private Text _timeRemining;
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private GameObject _indicatorDisplay;
        [SerializeField] private GameObject _counterDisplay;
        [SerializeField] private Transform _knifeSwitch;
        [SerializeField] private PlombPoint _transformerPoint;
        [SerializeField] private PlombPoint _keysPlomb;
        [SerializeField] private MeshRenderer[] _fourthStagePlombs;
        [SerializeField] private GameObject[] _offOnThirdStageGameObjects;
        [SerializeField] private GameObject[] _onOnThirdStageGameObjects;
        [SerializeField] private PlombPoint[] _plombPoints;
        [SerializeField] private IKK _ikk;
        [SerializeField] private CounterView _counterView;
        [SerializeField] private CableConnector _cableConnector;
        [SerializeField] private MeshRenderer[] _offItemsInThirdStage;
        [SerializeField] private SetupPanel _setupPanel;
        [SerializeField] private GameObject _secondPhaseAkt;
        [SerializeField] private GameObject _akt;
        [SerializeField] private ThirdStageSecondPhase _thirdStageSecondPhase;
        [SerializeField] private BreakingInstumentSevice _breakingInstumentSevice;
        [SerializeField] private Button _endStagesButton;
        [SerializeField] private Button _endThirdPhaseButton;
        [SerializeField] private Button _endSettup;
        [SerializeField] private Button _endFourthStageButton;
        [SerializeField] private ThirdStageController _thirdStageController;
        [SerializeField] private CE602MInstrument _cE602MInstrument;
        [SerializeField] private ElectricBoxView _electricBoxView;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _screwDriverPickdownButton;
        [SerializeField] private Button _capsPutAwayButton;
        [SerializeField] private Button _dismalatingPU;
        [SerializeField] private Button _endInspectionButton;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private PU _pu;

        private SelectedInventoryItemsObject _selectedInventoryItemsObject;
        private SecondStageState _secondStageState;
        private GameState _gameState;
        private InventoryPanel _inventoryPanel;
        private SecondStageModel _secondStageModel;
        private SecondStageController _secondStageController;
        private Inventory _inventory;
        private Timer _timer;
        [Inject] private FourthStageController _fourthStageController;
        [Inject] private FourthStageCableConnector _fourthStageCableConnector;
        [Inject] private FourthStageModel _fourthStageModel;
        [Inject] private FourthStageExamSystem _fourthStageExamSystem;
        [Inject] private FifthStageExam _fifthStageExam;

        public void Init(Inventory inventory,SecondStageController secondStageController,SecondStageModel secondStageModel,SecondStageState secondStageState,GameState gameState, InventoryPanel inventoryPanel)
        {
            _inventory = inventory;
            _secondStageController = secondStageController;
            _secondStageModel = secondStageModel;
            _secondStageState = secondStageState;
            _gameState = gameState;
            _inventoryPanel = inventoryPanel;
        }

        private void Awake()
        {
            _endFourthStageButton.onClick.AddListener(EndFourthStageEnterInFifth);
            _selectedInventoryItemsObject = FindObjectOfType<SelectedInventoryItemsObject>();
            _endSettup.gameObject.SetActive(false);
            _dismalatingPU.onClick.AddListener(DismalatingPU);
            _dismalatingPU.gameObject.SetActive(false);
            _endThirdPhaseButton.gameObject.SetActive(false);
            _endStagesButton.gameObject.SetActive(false);
            _inventoryButton.onClick.AddListener(OpenInventory);
            _capsPutAwayButton.onClick.AddListener(PutAwayCups);
            _endSettup.onClick.AddListener(EndSetup);
            _endStagesButton.onClick.AddListener(EndThirdPhase);
            _endThirdPhaseButton.onClick.AddListener(EndStages);
            _startFifthStageButton.onClick.AddListener(StartFifthStage);
            _screwDriverPickdownButton.onClick.AddListener(PickDownScrewdriwer);
            _timer = FindObjectOfType<Timer>();
        }

        public void OpenAktOfAddmission()
        {
            _secondPhaseAkt.SetActive(true);
        }

        public void EndAndReportThirdStage()
        {
            EndStages();
            _secondStageController.ResetThirdStage();
        }

        public void OpenDoors()
        {
            foreach (var item in _doors)
                item.Open();
        }

        public void EndSetup()
        {
            //if (!CheckToRightSetupScrews() || !_cableConnector.CheckToRightConnection())
            //{
            //    _secondStageController.EndSetupCaunterStage();
            //    EndStages();
            //    return;
            //}
            //_offTPPanel.SetActive(false);
            _indicatorDisplay.SetActive(false);
            _counterDisplay.SetActive(false);
            _breakingInstumentSevice.Disable();
            _secondStageController.CloseInstruments();
            _thirdStageSecondPhase.PrepareThirdPhase();
            _ikk.OpenWithOutScrewDriver();
            _counterView.OpenLidWithoutScrewdriver();
            _thirdStageSecondPhase.PreparePhase();
            _endSettup.gameObject.SetActive(false);
            //_endThirdPhaseButton.gameObject.SetActive(true);
            _endStagesButton .gameObject.SetActive(true);
            _cableConnector.CompleteCables();
            _ikkPoints.SetActive(true);
            foreach (var item in _offOnThirdStageGameObjects)
                item.SetActive(false);
            foreach (var item in _onOnThirdStageGameObjects)
                item.SetActive(true);
            foreach (var item in _plombPoints)
            {
                item.Close();
                item.OffColliders();
            }
            _ikkPoints.SetActive(true);
            _cableConnector.Deactive();
            _cableConnector.HideAllIndicatorsCables();
        }

        public void OnDismantlingPUButton()
        {

            //_dismalatingPU.gameObject.SetActive(false);
        }

        public void PickDownScrewdriwer()
        {
            _secondStageModel.PutAwayScrewDrivers();
            _screwDriverPickdownButton.gameObject.SetActive(false);
        }

        private void PutAwayCups()
        {
            _secondStageModel.PutAwayCups();
            _capsPutAwayButton.gameObject.SetActive(false);
        }

        private void DismalatingPU()
        {
            _pu.DismantlingPU();
            Invoke(nameof(EnterNextState), 0);
        }

        public void EndInspection()
        {
            _secondStageController.CloseInstruments();
            _inventory.BackSortedItemsToInventory();
            _cE602MInstrument.Close();
            _electricBoxView.PrepareBoxForSecondStage();
            _secondStageState.EnterInDismantlingState();
            _dismalatingPU.gameObject.SetActive(true);
            _endInspectionButton.gameObject.SetActive(false);
            _secondStageController.EndInspection();
            _secondStageController.TakeOffGloves();
        }

        public void AddObligatoryItems()
        {
            _inventory.AddObligatoryItems();
        }

        public void EnterInDosmatlingStage()
        {
            if(_selectedInventoryItemsObject != null)
                _selectedInventoryItemsObject.DeffectItemsToDeffect();
        }

        public void EnterNextState()
        {
            EnterNextState(true);
        }

        public void EndFourthStage()
        {
            foreach (var item in _shinaOff)
                item.SetActive(false);
            foreach(var item in _shinaOn)
                item.SetActive(true);
            _tpOn.SetActive(true);
            _knifeSwitch.Rotate(0, 180, 0);
            //_fourthStageExamSystem.RegisterExamSystem();
            _endFourthStageBackGround.SetActive(false);
            _plakatService.ResetPlakats();
            _powerSUP.gameObject.SetActive(true);
            _powerSUP.PlantWithoutFlag();
            _controller.gameObject.SetActive(true);
            _controller.PlantWithoutFlag();
            foreach (var item in _simPoints)
                item.TryDisplayIndicator();
            _powerIndicator.SetActive(true);
            _fourthStageCableConnector.ResetConnectionConnectAllCables();
            foreach(var item in _cablesFourthStage)
                item.SetActive(true);
            _transformerPoint.gameObject.SetActive(true);
            foreach (var item in _fourthStageScriptsPlombs)
                item.SetupPlomb();
            _transformerPoint.SetupCap();
            _ikkPlombPoint.SetupCap();
            _counterPlombPoint.SetupCap();
        }

        public void EnterNextState(bool reposetNextStage = true)
        {
            foreach (var item in _doors)
                item.Open();
            _selectedInventoryItemsObject.DeffectItemsToDeffect();
            _dismalatingPU.gameObject.SetActive(false);
            _secondStageController.CloseInstruments();
            _endSettup.gameObject.SetActive(true);
            foreach (var item in _offItemsInThirdStage)
                item.enabled = false;
            _akt.SetActive(true);
            _setupPanel.Open();
            _secondStageController.EnterInThirdStage();
            _breakingInstumentSevice.Disable();
            _breakingInstumentSevice.OffDeffects();
            _transformerPoint.Close();
            //_knifeSwitch.Rotate(0, 180, 0);
            _counterDisplay.SetActive(false);
            _indicatorDisplay.SetActive(false);
            _dismalatingPU.gameObject.SetActive(false);
            if(reposetNextStage)
                _secondStageController.EndDismalting();
            _plakatService.ResetPlakats();
            _inventory.BackAllItemsToInventory();
            _ikkPoints.SetActive(true);
            _thirdStageController.PrepareStage();
        }

        public void EndFourthStageEnterInFifth()
        {
            if(!_simPoints[0].IsIndicated && !_simPoints[1].IsIndicated)
            {
                _simPoints[0].SetupPoint();
                _simPoints[0].TryDisplayIndicator();
            }

            _antenaPoint.SetupAntena();
            foreach (var item in _plakatToOnForuthStage)
                item.SetActive(true);
            foreach (var item in _fourthStageScriptsPlombs)
                item.SetupPlomb();
            _aktReport.RightField();
            _fourthStageExamSystem.RegisterExamSystem();
            _computerButton.SetActive(true);
            _endFourthStageButton.gameObject.SetActive(false);
            _fourthStageController.ExitFromStage();
            _startFifthStageButton.gameObject.SetActive(true);
        }

        public void StartFifthStage()
        {
            //_computerButton.SetActive(false);
            _endThirdPhaseButton.gameObject.SetActive(true);
            _endFourthStageButton.gameObject.SetActive(false);
            _startFifthStageButton.gameObject.SetActive(false);
            _openPyramidButton.gameObject.SetActive(true);
        }

        public void EndThirdStageWithOutEnterFourthStage()
        {
            _inventory.BackThirdStageItemsToInventory();
            _keysPlomb.Close();
            _secondStageController.CloseInstruments();
            //_secondPhaseAkt.SetActive(true);
            //_endStagesButton.gameObject.SetActive(true);
            _endFourthStageButton.gameObject.SetActive(true);
            _endStagesButton.gameObject.SetActive(false);
            _ikk.CloseWithOutScrewDriver();
            _counterView.CloseLidWithoutScrewdriver();
            _ikk.EnableColladers();
            _counterView.EnableColladers();
            _keysPlomb.OnColliders();
            _secondStageController.EnterInLastPhaset();
            _breakingInstumentSevice.enabled = true;
            _breakingInstumentSevice.Enable();
            _breakingInstumentSevice.OffDeffects();
            _counterDisplay.SetActive(true);
            _indicatorDisplay.SetActive(true);
            _offTPPanel.SetActive(false);
            _fourthStageModel.ExitFromTP();
            foreach (var item in _plombPoints)
            {
                item.Close();
                item.OffColliders();
            }
            _ikkPoints.SetActive(true);
            _endFourthStageBackGround.SetActive(true);
            _aktReport.RightField();
        }

        public void EndThirdPhase()
        {
            _inventory.BackThirdStageItemsToInventory();
            //_keysPlomb.Close();
            _secondStageController.CloseInstruments();
            //_secondPhaseAkt.SetActive(true);
            //_endStagesButton.gameObject.SetActive(true);
            _endFourthStageButton.gameObject.SetActive(true);
            _endStagesButton.gameObject.SetActive(false);
            //_ikk.CloseWithOutScrewDriver();
            _counterView.CloseLidWithoutScrewdriver();
            //_ikk.EnableColladers();
            _counterView.EnableColladers();
            //_keysPlomb.OnColliders();
            _secondStageController.EnterInLastPhaset();
            _breakingInstumentSevice.enabled = true;
            _breakingInstumentSevice.Enable();
            _breakingInstumentSevice.OffDeffects();
            _counterDisplay.SetActive(true);
            _indicatorDisplay.SetActive(true);
            _offTPPanel.SetActive(false);
            _fourthStageModel.ExitFromTP();
            //foreach (var item in _plombPoints)
            //{
            //    item.Close();
            //    item.OffColliders();
            //}
            _ikkPoints.SetActive(true);
            _endFourthStageBackGround.SetActive(true);
            Invoke(nameof(EndFourthStage), 2f);
        }

        private bool CheckToRightSetupScrews()
        {
            if (_screwsMustToOff.Where(item => !item.enabled).ToArray().Length >= _screwsMustToOff.Length &&
                _screwsMustToOn.Where(item => item.enabled).ToArray().Length >= _screwsMustToOn.Length)
            {

                return true;
            }
            return false;
        }

        public void EndStages()
        {
            _fifthStageExam.RegisterFifthStageExam();
            _secondStageController.TryReportAllStages();

            ExamSystem.Instance.RegisterResult();

            _timer.GetOutReminingTime(out float minutes, out float seconds);
            _timer.StopTimer();
            _timeRemining.text = "ВРЕМЯ ОСТАЛОСЬ: " + string.Format("{0:00} : {1:00}",Mathf.Abs(minutes), Mathf.Abs(seconds));
            _resultPanel.SetActive(true);
            _score.text = "БАЛЛЫ: " + ExamSystem.Instance.Score;
        }

        private void OpenInventory()
        {
            _inventoryPanel.Open();
            Close();
        }

        public override void Open()
        {
            if (_secondStageModel.IsTakedScrewdriver)
                _screwDriverPickdownButton.gameObject.SetActive(true);
            else
                _screwDriverPickdownButton.gameObject.SetActive(false);

            if (_secondStageModel.IsTakedCaps)
                _capsPutAwayButton.gameObject.SetActive(true);
            else
                _capsPutAwayButton.gameObject.SetActive(false);

            _gameState.EnterInInspection();
            _panel.SetActive(true);
            base.Open();
        }

        public override void Close()
        {
            _panel.SetActive(false);
        }
    }
}