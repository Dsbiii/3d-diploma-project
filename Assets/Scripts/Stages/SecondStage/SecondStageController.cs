using Assets.Scripts.Instruments;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.FourthStage.SelectingCablesPanel;
using Assets.Scripts.Stages.FourthStage;
using Assets.Scripts.Stages.SecondStage.Dismantling;
using Assets.Scripts.Stages.SecondStage.Dismantling.Install_screw;
using Assets.Scripts.Stages.SecondStage.Electric_Box;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface;
using Assets.Scripts.Stages.SecondStage.Electric_Box.Parma;
using Assets.Scripts.Stages.SecondStage.Panels;
using Assets.Scripts.Stages.ThirdStage;
using Assets.Scripts.Stages.ThirdStage.CablesSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.SecondStage
{
    public class SecondStageController : MonoBehaviour
    {
        [SerializeField] private PlombPoint[] _counterPlombPoint;
        [SerializeField] private PlombPoint _ikkPlombPoint;
        [SerializeField] private PlombPoint _transfomrmatorPlombPoint;
        [SerializeField] private CableConnector _cableConnector;
        [SerializeField] private AktReport _dismantlingAktReport;
        [SerializeField] private AktReport _addmissionAktReport;
        [SerializeField] private GameObject _clamp;
        [SerializeField] private Act _act;
        [SerializeField] private ResetButtonService _resetButtonService;
        [SerializeField] private DragMagnite _dragMagnite;
        [SerializeField] private MeshRenderer _ikkLidMesh;
        [SerializeField] private MeshRenderer _counterLidMesh;

        [SerializeField] private MeteringParma _meteringParma;
        [SerializeField] private WatchService _watchService;
        [SerializeField] private ThirdStageModel _thirdStageModel;
        [SerializeField] private PlombPoint[] _counterPlombs;
        [SerializeField] private CapPoint[] _caps;
        [SerializeField] private Screw[] _screwsToOffOnDismalting;
        [SerializeField] private Screw[] _screwsTojumpersForCurrent;
        [SerializeField] private Screw[] _screwShorts;

        [SerializeField] private GerconInstrument _gerconInstrument;
        [SerializeField] private GameObject _ce602CanvasObject;
        [SerializeField] private CE602MInterface _cE602MInterface;
        [SerializeField] private PlakatService _plakat;
        [SerializeField] private Cable[] _cables;
        [SerializeField] private ElectricClampInstruments _electricClampInstruments;
        [SerializeField] private ParmaInstrument _parmaInstrument;
        [SerializeField] private CE602MInstrument _cE602MInstrument;
        [SerializeField] private CableScrewPicker _cableScrewPicker;

        private CapsService _capsService;
        private CounterView _counterView;
        private CE602MItemsPicker _cE602MItemsPicker;
        private IKK _iKK;
        private ItemPreview _firstStageItemPreview;
        private ItemPreviewPanel _itemPreviewPanel;
        private GameState _gameState;
        private SecondStageState _secondStageState;
        private FirstStageItemPicker _firstStageItemPicker;
        private SecondStageView _secondStageView;
        private SecondStageModel _secondStageModel;
        private DoorPicker _doorPicker;
        private SecondStagePanel _secondStagePanel;
        private InventoryPanel _inventoryPanel;
        private PlombService _plombService;
        private ParmaService _parmaService;
        private GerconService _gerconService;
        private CE602MService _cE602MService;
        private ElectricClampService _electricClampService;
        private List<Exam> _secondStageExams = new List<Exam>();
        private List<Exam> _secondStageDismantlingExams = new List<Exam>();
        private List<Exam> _thirdStageExams = new List<Exam>();

        private List<string> _examPassed = new List<string>();

        public static SecondStageController Instance { get; private set; }

        private bool _isOpenItem;
        private bool _isSecondPhase = true;
        private bool _isThirdStage;
        private string _necessarilyActionUser;
        private int _rightNecessarilyAction;
        private int _wrongNecessarilyAction;
        private bool _isTakedScrewDriverAfterTakedInstument;
        private bool _isTakedGloves;


        private string _priborsActionUser;
        private int _rightPriborAction;
        private int _wrongPriborAction;

        private bool _isRightPickedDielectricGlovesInspectionStage;
        private bool _isRightPickedCottonGlovesInspectionStage;
        private bool _isRightPickedHelmetInspectionStage;
        private bool _isRightPickedGlassesInspectionStage;
        private bool _isSelectedCups;
        private bool _isFirstSelectedCups;
        public bool IsAddmissionStage { get; private set; }
        public bool IsTakedParmaOrCE { get; private set; }
        public bool IsFirstPickedScrewDriver { get; private set; }
        public bool IsOpenDoors { get; private set; }
        private bool _isReportInspection;
        private bool _isReportDismantling;
        private bool _isReportSetupCounter;

        private void Awake()
        {
            Instance = GetComponent<SecondStageController>();
        }

        public void Init(
            PlombService plombService,
            CapsService capsService,
            CounterView counterView,
            InventoryPanel inventoryPanel,
            CE602MItemsPicker cE602MItemsPicker,
            IKK iKK,
            SecondStageModel secondStageModel,
            SecondStagePanel secondStagePanel,
            DoorPicker doorPicker,
            ItemPreviewPanel itemPreviewPanel,
            ItemPreview firstStageItemPreview,
            SecondStageView secondStageView,
            GameState gameState,
            FirstStageItemPicker firstStageItemPicker,
            SecondStageState secondStageState)
        {
            _plombService = plombService;
            _capsService = capsService;
            _counterView = counterView;
            _inventoryPanel = inventoryPanel;
            _cE602MItemsPicker = cE602MItemsPicker;
            _iKK = iKK;
            _secondStageState = secondStageState;
            _secondStagePanel = secondStagePanel;
            _itemPreviewPanel = itemPreviewPanel;
            _secondStageModel = secondStageModel;
            _firstStageItemPreview = firstStageItemPreview;
            _gameState = gameState;
            _firstStageItemPicker = firstStageItemPicker;
            _doorPicker = doorPicker;
            _secondStageView = secondStageView;
            _doorPicker = doorPicker;
            _parmaService = new ParmaService(_meteringParma,_resetButtonService,_gerconInstrument, _itemPreviewPanel, _electricClampInstruments, _parmaInstrument, _cE602MInstrument, secondStagePanel, firstStageItemPreview);
            _cE602MService = new CE602MService(_resetButtonService, _gerconInstrument, _cE602MInterface, _itemPreviewPanel, _electricClampInstruments, _parmaInstrument, _cE602MInstrument, secondStagePanel, firstStageItemPreview);
            _electricClampService = new ElectricClampService(_resetButtonService, _gerconInstrument, _itemPreviewPanel, _electricClampInstruments, _parmaInstrument, _cE602MInstrument, secondStagePanel, firstStageItemPreview);
            _gerconService = new GerconService(_gerconInstrument, _cE602MInterface, itemPreviewPanel, _electricClampInstruments, _parmaInstrument, _cE602MInstrument, secondStagePanel, firstStageItemPreview);

            _itemPreviewPanel.InitFromController(_parmaService, _cE602MService);
            _plakat.InitFromController(_parmaService, _cE602MService);

            var selectedInventoryItemsObject = FindObjectOfType<SelectedInventoryItemsObject>();
            Debug.Log("selectedInventoryItemsObject " + selectedInventoryItemsObject);
            if (selectedInventoryItemsObject != null && selectedInventoryItemsObject.Items != null)
            {
                foreach (var item in selectedInventoryItemsObject.Items)
                {
                    Debug.Log("item " + item);
                    _secondStageModel.AddItem(item);
                }
            }
            _secondStageView.InitInventory(_secondStageModel.Items);
            _cableConnector.Init(_cE602MService, _parmaService);
        }

        public void AddExamPassed(string examName)
        {
            _examPassed.Add(examName);  
        }


        public void TakedCups()
        {
            _isFirstSelectedCups = true;
            _isSelectedCups = true;
        }

        public void PutCaps()
        {
            _isSelectedCups = false;
        }

        public void AddSecondStageExam(string name,string action , string idealAction , int right , int wrong)
        {
            _secondStageExams.Add(new Exam(wrong, right, idealAction, action, name));
        }
        public void AddSecondStageExam(string name,string action , string idealAction ,string result)
        {
            _secondStageExams.Add(new Exam(result, idealAction, action, name));
        }

        public void AddThirdStageExam(string name, string action, string idealAction, string result)
        {
            _thirdStageExams.Add(new Exam(result, idealAction, action, name));
        }

        public void AddSecondStageDismantlingExam(string name, string action, string idealAction, int right, int wrong)
        {
            _secondStageDismantlingExams.Add(new Exam(wrong, right, idealAction, action, name));
        }
        public void AddThirdStageExam(string name, string action, string idealAction, int right, int wrong)
        {
            _thirdStageExams.Add(new Exam(wrong, right, idealAction, action, name));
        }
        public void TakedRightPickedDielectricGlovesInspectionStage()
        {
            if(!_meteringParma.IsUssed)
                _isRightPickedDielectricGlovesInspectionStage = true;
        }
        public void TakedRightPickedCottonGlovesInspectionStage()
        {
            _isRightPickedCottonGlovesInspectionStage = true;
        }
        public void TakedRightPickedGlassesInspectionStage()
        {
            _isRightPickedGlassesInspectionStage = true;
        }
        public void TakedRightPickedHelmetInspectionStage()
        {
            _examPassed.Add("_isRightPickedHelmetInspectionStage");
            _isRightPickedHelmetInspectionStage = true;
        }

        public void AddNecessarilyAction(string action, int wrong , int right)
        {
            _wrongNecessarilyAction += wrong;
            _rightNecessarilyAction += right;
            _necessarilyActionUser += action;
        }

        public void AddPriborAction(string action, int wrong, int right)
        {
            _wrongPriborAction += wrong;
            _rightPriborAction += right;
            _priborsActionUser += action;
        }

        private void PrepareInstruments()
        {
            _parmaService.PrepareToOpen();
            _cE602MService.PrepareToOpen();
            _gerconService.PrepareToOpen();
            _electricClampService.PrepareToOpen();
        }

        public void EnterInThirdStage()
        {
            _isThirdStage = true;
        }

        public void EnterInThirdPhaseThirdStage()
        {
            _isSecondPhase = true;
            _secondStageModel.PutAwayPlombs();
        }

        public void TryReportAllStages(bool resetAll = false)
        {
            bool isReportInspection = _isReportInspection;
            bool isReportDismantling = _isReportDismantling;
            bool isReportSetupCounter = _isReportSetupCounter;

            if (!_isReportInspection)
            {
                Debug.Log("Report Inspection");
                //_wrongReportPlakatSecondStage = true;
                ReportSelectObyDeystviy reportSelectObyDeystviy = FindObjectOfType<ReportSelectObyDeystviy>();
                if (reportSelectObyDeystviy != null )
                {
                    reportSelectObyDeystviy.Select();
                }

                EndInspection();
                if (resetAll)
                {
                    foreach(var item in _secondStageExams)
                    {
                        item.UserAction = "";
                    }
                }
            }
            if (!_isReportDismantling)
            {
                Debug.Log("Report Dismantling");
                //_wrongReportPlakatSecondStage = true;
                EndDismalting();

                if (resetAll)
                {
                    foreach (var item in _secondStageDismantlingExams)
                    {
                        item.UserAction = "";
                    }
                }
            }
            if (!_isReportSetupCounter)
            {
                Debug.Log("Report SetupCounter");
                //_wrongReportPlakatThirdStage = true;
                if(_isThirdStage)
                    EndSetupCaunterStage();
                else
                    EndSetupCaunterStage(true);

                if (resetAll)
                {
                    foreach (var item in _thirdStageExams)
                    {
                        item.UserAction = "";
                    }
                }
            }
        }

        public void EndInspection(bool isEmpty = false)
        {
            //ExamSystem.Instance.AddExam(new Exam("Демонтаж счетчика"));
            bool isRightUseParma = false;
            bool isHaveCriticalError = false;

            //if (_examPassed.Contains("_isRightPickedHelmetInspectionStage") && _isRightPickedHelmetInspectionStage)
            //    AddSecondStageExam("Использование каски" , "Правильно", "Взять-осмотреть- заменить при необходимости - надеть", 1, 0);
            //else if(_examPassed.Contains("_isRightPickedHelmetInspectionStage"))
            //    AddSecondStageExam("Использование каски", "Ошибка", "Взять-осмотреть- заменить при необходимости - надеть", 0, 0);
            //else
            //    AddSecondStageExam("Использование каски", "", "Взять-осмотреть- заменить при необходимости - надеть", 0, 0);

            //if (_examPassed.Contains("_isRightPickedCottonGlovesInspectionStage") && _isRightPickedCottonGlovesInspectionStage)
            //    AddSecondStageExam("Использование х/б перчаток", "Правильно", "Взять-осмотреть- заменить при необходимости - надеть", 1, 0);
            //else if(_examPassed.Contains("_isRightPickedCottonGlovesInspectionStage"))
            //    AddSecondStageExam("Использование х/б перчаток", "Ошибка", "Взять-осмотреть- заменить при необходимости - надеть", 0, 0);
            //else
            //    AddSecondStageExam("Использование х/б перчаток", "", "Взять-осмотреть- заменить при необходимости - надеть", 0, 0);

            //_plakat.ReportPlakatsForSecondStage();

            //if (_examPassed.Contains("IsTakedScrewdriver") && _ikkLidMesh.enabled == false && _counterLidMesh.enabled == false && !_secondStageModel.IsTakedScrewdriver)
            //    AddSecondStageExam("Осмотр(использование отвертки)", "Правильно", "Проведение осмотра со снятием пломб и крышек ИКК и счетчика. По окончанию процедуры отвертка убирается", 1, 0);
            //else if (_examPassed.Contains("IsTakedScrewdriver"))
            //    AddSecondStageExam("Осмотр(использование отвертки)", "Ошибка", "Проведение осмотра со снятием пломб и крышек ИКК и счетчика. По окончанию процедуры отвертка убирается", 0, 0);
            //else
            //    AddSecondStageExam("Осмотр(использование отвертки)", "", "Проведение осмотра со снятием пломб и крышек ИКК и счетчика. По окончанию процедуры отвертка убирается", 0, 0);

            //if (_examPassed.Contains("_isRightPickedDielectricGlovesInspectionStage") && _isRightPickedDielectricGlovesInspectionStage)
            //    AddSecondStageExam("Диэлектрические перчатки ", "Правильно", "Взять-осмотреть- проверить-заменить при необходимости - надеть", 1, 0);
            //else if(_examPassed.Contains("_isRightPickedDielectricGlovesInspectionStage"))
            //{
            //    isHaveCriticalError = true;
            //    AddSecondStageExam("Диэлектрические перчатки ", "Критическая ошибка", " - одеть диэлектрические перчатки – до взятия отвертки;", 0, 0);
            //}
            //else
            //{
            //    AddSecondStageExam("Диэлектрические перчатки ", "", " - одеть диэлектрические перчатки – до взятия отвертки;", 0, 0);
            //}

            if (_isRightPickedHelmetInspectionStage)
                AddSecondStageExam("Использование каски", "Правильно", "Взять-осмотреть- заменить при необходимости - надеть", 1, 0);
            else
                AddSecondStageExam("Использование каски", "Ошибка", "Взять-осмотреть- заменить при необходимости - надеть", 0, 0);

            if (_isRightPickedCottonGlovesInspectionStage)
                AddSecondStageExam("Использование х/б перчаток", "Правильно", "Взять-осмотреть- заменить при необходимости - надеть", 1, 0);
            else
                AddSecondStageExam("Использование х/б перчаток", "Ошибка", "Взять-осмотреть- заменить при необходимости - надеть", 0, 0);

            _plakat.ReportPlakatsForSecondStage();

            if (_ikkLidMesh.enabled == false && _counterLidMesh.enabled == false && !_secondStageModel.IsTakedScrewdriver)
                AddSecondStageExam("Осмотр(использование отвертки)", "Правильно", "Проведение осмотра со снятием пломб и крышек ИКК и счетчика. По окончанию процедуры отвертка убирается", 1, 0);
            else
            {

                AddSecondStageExam("Осмотр(использование отвертки)", "Ошибка", "Проведение осмотра со снятием пломб и крышек ИКК и счетчика. По окончанию процедуры отвертка убирается", 0, 0);
            }

            if (_isRightPickedDielectricGlovesInspectionStage)
                AddSecondStageExam("Диэлектрические перчатки ", "Правильно", "Взять-осмотреть- проверить-заменить при необходимости - надеть", 1, 0);
            else
            {
                isHaveCriticalError = true;
                AddSecondStageExam("Диэлектрические перчатки ", "Критическая ошибка", " - одеть диэлектрические перчатки – до взятия отвертки;", 0, 0);
            }

            if (_parmaService.IsWasTaked  &&  _meteringParma.IsUssed && _parmaService.IsBackedInBag && _meteringParma.IsUssedStopWatch)
            {
                isRightUseParma = true;
                if(!isHaveCriticalError)
                    AddSecondStageExam("Использование Пармы-ВАФ ", "Правильно", "Взять- измерить- снять все щупы и клещи-убрать в сумку", 2, 0);
                else
                    AddSecondStageExam("Использование Пармы-ВАФ ", "Правильно", "Взять- измерить- снять все щупы и клещи-убрать в сумку", 0, 0);
            }
            else if(_parmaService.IsWasTaked && _meteringParma.IsUssed && _parmaService.IsBackedInBag && !_meteringParma.IsUssedStopWatch)
            {
                isRightUseParma = true;
                if (!isHaveCriticalError)
                    AddSecondStageExam("Использование Пармы-ВАФ ", "Правильно", "Взять- измерить- снять все щупы и клещи-убрать в сумку", 1, 0);
                else
                    AddSecondStageExam("Использование Пармы-ВАФ ", "Правильно", "Взять- измерить- снять все щупы и клещи-убрать в сумку", 0, 0);
            }
            else
            {
                AddSecondStageExam("Использование Пармы-ВАФ", "Ошибка", "Взять- измерить- снять все щупы и клещи-убрать в сумку", 0, 0);
            }


            if (_meteringParma.IsUssedStopWatch )
                AddSecondStageExam("использование секундомера ", "Правильно", "Взять- провести измерение- убрать в сумку", "Да");
            else
            {
                AddSecondStageExam("использование секундомера", "Ошибка", "Взять- провести измерение- убрать в сумку","Нет");
            }

            if (_meteringParma.IsUssedClamps && !_clamp.activeSelf)
            {
                if (!isHaveCriticalError)
                    AddSecondStageExam("Использование токовых клещей ", "Правильно", "взять- провести измерения-снять с шины - убрать в сумку", 1, 0);
                else
                    AddSecondStageExam("Использование токовых клещей ", "Правильно", "взять- провести измерения-снять с шины - убрать в сумку", 0, 0);
            }
            else
            {
                AddSecondStageExam("Использование токовых клещей ", "Ошибка", "взять- провести измерения-снять с шины - убрать в сумку", 0, 0);
            }

            if (_meteringParma.IsUssed && _cE602MInterface.IsOpenFirst && _cE602MInterface.IsFirstableRightConnect && !_cE602MService.IsOpen)
            {
                if (!isHaveCriticalError)
                {
                    if (isRightUseParma)
                        AddSecondStageExam("Использование СЕ602М", "Правильно", "взять- правильно у становить все щупы, клещи и т.д. - провести измерения- снять все щупы, клещи и т.д.- убрать в сумку", 0, 0);
                    else
                        AddSecondStageExam("Использование СЕ602М", "Правильно", "взять- правильно у становить все щупы, клещи и т.д. - провести измерения- снять все щупы, клещи и т.д.- убрать в сумку", 2, 0);
                }
                else
                {
                    AddSecondStageExam("Использование СЕ602М", "Правильно", "взять- правильно у становить все щупы, клещи и т.д. - провести измерения- снять все щупы, клещи и т.д.- убрать в сумку", 0, 0);
                }

            }
            else
                AddSecondStageExam("Использование СЕ602М", "Ошибка", "взять- правильно у становить все щупы, клещи и т.д. - провести измерения- снять все щупы, клещи и т.д.- убрать в сумку", 0, 0);

            if (_gerconService.IsWasTaked && !_gerconService.IsOpen)
                AddSecondStageExam("Поиск Геркона", "Правильно", "взять магнит- провести поиск геркона-убрать магнит", 1, 0);
            else
                AddSecondStageExam("Поиск Геркона", "Ошибка", "взять магнит- провести поиск геркона-убрать магнит", 0, 0);

            //if (isHaveCriticalError)
            //{
            //    foreach (var exam in _secondStageDismantlingExams)
            //        exam.ScoreForExam = 0;
            //    foreach(var exam in _fourthStageExams)
            //        exam.ScoreForExam = 0;
            //}

            if (isEmpty)
            {
                foreach (var exam in _secondStageDismantlingExams)
                    exam.UserAction = "";
                foreach (var exam in _secondStageExams)
                    exam.UserAction = "";
            }

            //foreach (var exam in _secondStageExams)
            //    ExamSystem.Instance.AddExam(exam);

            _isReportInspection = true;
            //_isRightPickedCottonGlovesInspectionStage = false;
            //_isRightPickedDielectricGlovesInspectionStage = false;
            //_isRightPickedGlassesInspectionStage = false;
            //_isRightPickedHelmetInspectionStage = false;
            ResetActions();

        }


        private void ResetActions()
        {
            _necessarilyActionUser = "";
            _rightNecessarilyAction = 0;
            _wrongNecessarilyAction = 0;
            _priborsActionUser = "";
            _rightPriborAction = 0;
            _wrongPriborAction = 0;

            _isTakedScrewDriverAfterTakedInstument = false;

            _parmaService.ResetIsWasTaked();
            _cE602MService.ResetIsWasTaked();
            _electricClampService.ResetIsWasTaked();
            _gerconService.ResetIsWasTaked();

            IsTakedParmaOrCE = false;
            IsFirstPickedScrewDriver = false;
            IsOpenDoors = false;
        }

        public void EndDismalting(bool isEmpty = false)
        {
            Debug.Log("EndDismalting");

            bool isRightCupsAndCableConnection = true;
            bool isHaveCriticalError = false;


            if (_dismantlingAktReport.CheckForRightFillAkt())
                AddSecondStageDismantlingExam("Заполнение Акта", "Правильно", "Полностью и правильно заполнить акт", 6, 0);
            else
                AddSecondStageDismantlingExam("Заполнение Акта", "Ошибка", "Полностью и правильно заполнить акт", 0, 0);

            if (_isRightPickedGlassesInspectionStage)
                AddSecondStageDismantlingExam("Использование защитных очков", "Правильно", "Взять-осмотреть- заменить при необходимости - надеть", 1, 0);
            else
            {
                isHaveCriticalError = true;
                AddSecondStageDismantlingExam("Использование защитных очков", "Критическая ошибка", "Взять-осмотреть- заменить при необходимости - надеть", 0, 0);
            }

            if (_isRightPickedDielectricGlovesInspectionStage)
                AddSecondStageDismantlingExam("Диэлектрические перчатки ", "Правильно", "Взять-осмотреть- проверить-заменить при необходимости - надеть", 1, 0);
            else
            {
                isHaveCriticalError = true;

                AddSecondStageDismantlingExam("Диэлектрические перчатки ", "Критическая ошибка", " - одеть диэлектрические перчатки – до взятия отвертки;", 0, 0);
            }

            if (_screwsTojumpersForCurrent.Where(item => !item.IsOpen).ToArray().Length >= _screwsTojumpersForCurrent.Length
                    && _screwsToOffOnDismalting.Where(item => item.IsOpen).ToArray().Length >= _screwsToOffOnDismalting.Length)
                AddSecondStageDismantlingExam("Установка закороток, перемычек, убрать перемычки в ИКК", "Правильно", "Взять отвертку- убрать перемычки напряжения-поставить закоротки и токовые перемычки", 1, 0);
            else
            {
                isHaveCriticalError = true;

                AddSecondStageDismantlingExam("Установка закороток, перемычек, убрать перемычки в ИКК", "Критическая ошибка", "Взять отвертку- убрать перемычки напряжения-поставить закоротки и токовые перемычки", 0, 0);
            }

            if (_parmaService.IsWasTaked && _meteringParma.IsUssed && _parmaService.IsBackedInBag && _meteringParma.IsUssedStopWatch)
            {
                AddSecondStageDismantlingExam("Проверка Пармой-ВАФ перед отключением", "Правильно", "Взять- измерить- снять все щупы и клещи-убрать в сумку", 1, 0);
            }
            else
            {
                AddSecondStageDismantlingExam("Проверка Пармой-ВАФ перед отключением", "Ошибка", "Взять- измерить- снять все щупы и клещи-убрать в сумку", 0, 0);
            }

            if (_cables.Where(item => item.IsPullOut).ToArray().Length >= _cables.Length &&
                _caps.Where(item => item.IsActiveCap).ToArray().Length >= _caps.Length)
                AddSecondStageDismantlingExam("Отключение проводки счетчика", "Правильно", "Взять отвертку и защитные колапачки-извлечь провод-надеть колкачок-все 10 проводов", 1, 0);
            else
            {
                isHaveCriticalError = true;

                AddSecondStageDismantlingExam("Отключение проводки счетчика", "критическая ошибка", "Взять отвертку и защитные колапачки-извлечь провод-надеть колкачок-все 10 проводов", 0, 0);
            }

            

            if (!_secondStageModel.IsTakedScrewdriver && !_secondStageModel.IsTakedCaps && _secondStageModel.IsFristSelectedCaps)
                AddSecondStageDismantlingExam("Убрать рабочее место", "Правильно", "убрать отвертку- убрать защитные колпачки", 1, 0);
            else
                AddSecondStageDismantlingExam("Убрать рабочее место", "Ошибка", "убрать отвертку- убрать защитные колпачки", 0, 0);

            if (isHaveCriticalError)
            {
                foreach (var exam in _secondStageDismantlingExams)
                    exam.ScoreForExam = 0;
                foreach (var exam in _secondStageExams)
                    exam.ScoreForExam = 0;
            }

            if (isEmpty)
            {
                foreach (var exam in _secondStageExams)
                    exam.UserAction = "";
                foreach (var exam in _secondStageDismantlingExams)
                    exam.UserAction = "";
            }


            _isTakedScrewDriverAfterTakedInstument = false;
            _isTakedGloves = false;
            //ExamSystem.Instance.AddExam(_wrongNecessarilyAction, _rightNecessarilyAction, "- одеть диэлектрические перчатки – до взятия отвертки; - одеть очки – до взятия отвертки; - Взял Парму-Убрал Парму;",
            //    _necessarilyActionUser, "Демонтаж счетчика");

            _isReportDismantling = true;
            //foreach (var exam in _secondStageDismantlingExams)
            //    ExamSystem.Instance.AddExam(exam);
            _isRightPickedCottonGlovesInspectionStage = false;
            _isRightPickedDielectricGlovesInspectionStage = false;
            _isRightPickedGlassesInspectionStage = false;
            _isRightPickedHelmetInspectionStage = false;
            ResetActions();
            _meteringParma.ResetUsingParma();
            _cE602MInterface.RessetUsingCE602M();
            _dragMagnite.ResetUsingMagnite();

            IsAddmissionStage = true;
        }

        public void EndSetupCaunterStage(bool isEmpty = false)
        {
            //ExamSystem.Instance.AddExam(new Exam("Установка счетчика"));

            bool isRightUseParma = false;
            bool isHaveCriticalError = false;

            if (_isRightPickedCottonGlovesInspectionStage)
                AddThirdStageExam("Использование х/б перчаток", "Правильно", "Взять-осмотреть- заменить при необходимости - надеть", 1, 0);
            else
                AddThirdStageExam("Использование х/б перчаток", "Ошибка", "Взять-осмотреть- заменить при необходимости - надеть", 0, 0);

            if (_isRightPickedHelmetInspectionStage)
                AddThirdStageExam("Использование каски", "Правильно", "Взять-осмотреть- заменить при необходимости - надеть", 1, 0);
            else
                AddThirdStageExam("Использование каски", "Ошибка", "Взять-осмотреть- заменить при необходимости - надеть", 0, 0);

            if (_thirdStageModel.IsPlantedCounter && _thirdStageModel.IsRightSelectedCounter)
                AddThirdStageExam("выбор счетчика", "Правильно", "правильно выбран- правильно установлен", 1, 0);
            else
            {
                isHaveCriticalError = true;

                AddThirdStageExam("выбор счетчика", "Критическая ошибка", "правильно выбран- правильно установлен", 0, 0);
            }
            if (_thirdStageModel.IsPlantedTransformers && _thirdStageModel.IsRightSelectedTransformers)
                AddThirdStageExam("Выбор трансформаторов", "Правильно", "правильно выбран- правильно установлен", 1, 0);
            else
            {
                isHaveCriticalError = true;

                AddThirdStageExam("Выбор трансформаторов", "Критическая ошибка", "правильно выбран- правильно установлен", 0, 0);
            }
            if (_thirdStageModel.IsPlatedIKK)
                AddThirdStageExam("Выбор и подготовка ИКК", "Правильно", "правильно выбран- правильно установлен", 1, 0);
            else
            {
                isHaveCriticalError = true;

                AddThirdStageExam("Выбор и подготовка ИКК", "Критическая ошибка", "правильно выбран- правильно установлен", 0, 0);
            }

            if (_thirdStageModel.IsRightSelectedCables && _cableConnector.CheckToRightConnection())
                AddThirdStageExam("Выбор проводов и правильность подключения", "Правильно", "не нарушена последовательность(все элементы установлены)-правильно выбран- правильно подключены все устройства", 2, 0);
            else
            {
                isHaveCriticalError = true;

                AddThirdStageExam("Выбор проводов и правильность подключения", "Критическая ошибка", "не нарушена последовательность(все элементы установлены)-правильно выбран- правильно подключены все устройства", 0, 0);
            }

            //if (_thirdStageModel.IsSetupedCables && _thirdStageModel.IsRightSelectedCables && _cableConnector.CheckToRightConnection())
            //    AddThirdStageExam("Выбор проводов и правильность подключения", "Правильно", "не нарушена последовательность(все элементы установлены)-правильно выбран- правильно подключены все устройства", 2, 0);
            //else
            //{
            //    isHaveCriticalError = true;

            //    AddThirdStageExam("Выбор проводов и правильность подключения", "Критическая ошибка", "не нарушена последовательность(все элементы установлены)-правильно выбран- правильно подключены все устройства", 0, 0);
            //}
            if (_thirdStageModel.IsMarkered)
                AddThirdStageExam("Маркирование", "Правильно", "не нарушена последовательность(все элементы установлены)", 1, 0);
            else
                AddThirdStageExam("Маркирование", "Ошибка", "не нарушена последовательность(все элементы установлены)", 0, 0);


            if (_transfomrmatorPlombPoint.IsActivePlomb)
                AddThirdStageExam("Пломбирование трансформаторов", "Правильно", "сделано до вывода ТП", 1, 0);
            else
                AddThirdStageExam("Пломбирование трансформаторов", "Ошибка", "сделано до вывода ТП", 0, 0);

            _plakat.ReportPlakatsForThirdStage();


            if (_isRightPickedDielectricGlovesInspectionStage)
                AddThirdStageExam("Использование диэлектрических перчаток", "Правильно", "Взять-осмотреть- заменить при необходимости - надеть", 1, 0);
            else
            {
                isHaveCriticalError = true;
                AddThirdStageExam("Использование диэлектрических перчаток", "Критическая ошибка", "Взять-осмотреть- заменить при необходимости - надеть", 0, 0);
            }

            if (_parmaService.IsWasTaked && _meteringParma.IsUssed && _parmaService.IsBackedInBag && _meteringParma.IsUssedStopWatch)
            {
                isRightUseParma = true;
                AddThirdStageExam("Использование Пармы-ВАФ ", "Правильно", "Взять- измерить- снять все щупы и клещи-убрать в сумку", 2, 0);
            }
            else if (_parmaService.IsWasTaked && _meteringParma.IsUssed && _parmaService.IsBackedInBag && !_meteringParma.IsUssedStopWatch)
            {
                isRightUseParma = true;
                AddThirdStageExam("Использование Пармы-ВАФ ", "Правильно", "Взять- измерить- снять все щупы и клещи-убрать в сумку", 1, 0);
            }
            else
            {
                AddThirdStageExam("Использование Пармы-ВАФ", "Ошибка", "Взять- измерить- снять все щупы и клещи-убрать в сумку", 0, 0);
            }


            if (_meteringParma.IsUssedStopWatch)
                AddThirdStageExam("использование секундомера ", "Правильно", "Взять- провести измерение- убрать в сумку", "Да");
            else
            {
                AddThirdStageExam("использование секундомера", "Ошибка", "Взять- провести измерение- убрать в сумку", "Нет");
            }

            if (_meteringParma.IsUssedClamps && !_clamp.activeSelf)
                AddThirdStageExam("Использование токовых клещей ", "Правильно", "взять- провести измерения-снять с шины - убрать в сумку", 1, 0);
            else
            {
                AddThirdStageExam("Использование токовых клещей ", "Ошибка", "взять- провести измерения-снять с шины - убрать в сумку", 0, 0);
            }

            if (_meteringParma.IsUssed && _cE602MInterface.IsOpenFirst && _cE602MInterface.IsFirstableRightConnect && !_cE602MService.IsOpen)
            {
                if (isRightUseParma)
                    AddThirdStageExam("Использование СЕ602М", "Правильно", "взять- правильно у становить все щупы, клещи и т.д. - провести измерения- снять все щупы, клещи и т.д.- убрать в сумку", 0, 0);
                else
                    AddThirdStageExam("Использование СЕ602М", "Правильно", "взять- правильно у становить все щупы, клещи и т.д. - провести измерения- снять все щупы, клещи и т.д.- убрать в сумку", 2, 0);
            }
            else
                AddThirdStageExam("Использование СЕ602М", "Ошибка", "взять- правильно у становить все щупы, клещи и т.д. - провести измерения- снять все щупы, клещи и т.д.- убрать в сумку", 0, 0);

            if (_gerconService.IsWasTaked && !_gerconService.IsOpen)
                AddThirdStageExam("Поиск Геркона", "Правильно", "взять магнит- провести поиск геркона-убрать магнит", 1, 0);
            else
                AddThirdStageExam("Поиск Геркона", "Ошибка", "взять магнит- провести поиск геркона-убрать магнит", 0, 0);

            if (_ikkPlombPoint.IsActivePlomb)
                AddThirdStageExam("Устанока плобы на ИКК", "Правильно", "сделано до вывода ТП", 1, 0);
            else
                AddThirdStageExam("Устанока плобы на ИКК", "Ошибка", "сделано до вывода ТП", 0, 0);

            if (_counterPlombPoint.Where(item => item.IsActivePlomb).ToArray().Length == _counterPlombPoint.Length)
                AddThirdStageExam("Устанока плобы на клемную крышку счетчика", "Правильно", "установка пломбы", 1, 0);
            else
                AddThirdStageExam("Устанока плобы на клемную крышку счетчика", "Ошибка", "установка пломбы", 0, 0);

            if (_addmissionAktReport.CheckForRightFillAkt())
                AddThirdStageExam("Заполнение Акта", "Правильно", "Полностью и правильно заполнить акт", 6, 0);
            else
                AddThirdStageExam("Заполнение Акта", "Ошибка", "Полностью и правильно заполнить акт", 0, 0);

            if (isHaveCriticalError)
            {
                foreach (var exam in _thirdStageExams)
                    exam.ScoreForExam = 0;
            }

            if (isEmpty)
            {
                foreach (var exam in _thirdStageExams)
                    exam.UserAction = "";
            }

            //if (_isRightPickedDielectricGlovesInspectionStage && !IsFirstPickedScrewDriver)
            //    AddNecessarilyAction("- одеть диэлектрические перчатки – до взятия отвертки;", 0, 1);
            //else
            //    AddNecessarilyAction("", 1, 0);

            //if (_isRightPickedCottonGlovesInspectionStage)
            //    AddNecessarilyAction("осмотреть и одеть х/б перчатки- до открытия дверей ТП;", 0, 1);
            //else
            //    AddNecessarilyAction("", 1, 0);

            //if (_isRightPickedHelmetInspectionStage)
            //    AddNecessarilyAction("осмотреть и одеть каску- до открытия дверей ТП;", 0, 1);
            //else
            //    AddNecessarilyAction("", 1, 0);
            //ExamSystem.Instance.AddExam(_wrongNecessarilyAction, _rightNecessarilyAction, "- одеть диэлектрические перчатки – до взятия отвертки; осмотреть и одеть х/б перчатки- до открытия дверей ТП; осмотреть и одеть каску- до открытия дверей ТП; - осмотреть и одеть очки – до взятия отвертки;",
            //    _necessarilyActionUser, "обязательные действия");
            //_necessarilyActionUser = "";
            //_rightNecessarilyAction = 0;
            //_wrongNecessarilyAction = 0;

            //if (_counterPlombs.Where(item => item.IsPlombed).ToArray().Length >= _counterPlombs.Length)
            //    AddThirdStageExam("Опломбирование трансформаторов" ,"Опломбировал трансформаторы" , "Опломбировал трансформаторы", 1, 0);
            //else
            //    AddThirdStageExam("Опломбирование трансформаторов", "", "Опломбировал трансформаторы", 0, 1);

            //_plakat.ReportPlakatsWithoutInstruments();

            //if (!_secondStageModel.IsTakedScrewdriver && IsFirstPickedScrewDriver)
            //    AddThirdStageExam("Отвертка", "Взять Убрать отвертку", "Взять Убрать отвертку", 1, 0);
            //else
            //    AddThirdStageExam("Отвертка", "", "Взять Убрать отвертку", 0, 1);

            //if (_ikkLidMesh.enabled == false)
            //    AddThirdStageExam("Клемная крышка ИКК при осмотре нового ИКК", "Снял клеменую крышку ИКК", "Снял клеменую крышку ИКК", 1, 0);
            //else
            //    AddThirdStageExam("Клемная крышка ИКК при осмотре нового ИКК", "", "Снял клеменую крышку ИКК", 0, 1);

            //if (_meteringParma.IsUssed)
            //    AddThirdStageExam("Использование Парма-ВАФ", "Использовал Парма-ВАФ", "Использовал Парма-ВАФ", 1, 0);
            //else
            //    AddThirdStageExam("Использование Парма-ВАФ", "", "Использовал Парма-ВАФ", 0, 1);

            //if (_cE602MInterface.IsOpenFirst && _cE602MInterface.IsFirstableRightConnect)
            //    AddThirdStageExam("Использование CE602M", "Использовал CE602M", "Использовал CE602M", 1, 0);
            //else
            //    AddThirdStageExam("Использование CE602M", "", "Использовал CE602M", 0, 1);

            //if (_meteringParma.IsUssed && _cE602MInterface.IsOpenFirst && _cE602MInterface.IsFirstableRightConnect)
            //    AddThirdStageExam("Использование CE602M и Парма-ВАФ", "Использовал CE602M и Парма-ВАФ", "Использовал CE602M", 1, 0);
            //else
            //    AddThirdStageExam("Использование CE602M", "", "Использовал CE602M и Парма-ВАФ ", 0, 1);

            //if (_dragMagnite.IsFindingGercon)
            //    AddThirdStageExam("Поиск геркона", "Использовал магнит", "Использовал магнит", 1, 0);
            //else
            //    AddThirdStageExam("Поиск геркона", "", "Использовал магнит", 0, 1);

            //AddThirdStageExam("Установка", "Установка", "Установка", 0, 0);

            _isReportSetupCounter = true;
            _priborsActionUser = "";
            _rightPriborAction = 0;
            _wrongPriborAction = 0;

            //foreach (var exam in _thirdStageExams)
            //    ExamSystem.Instance.AddExam(exam);
        }

        public void ResetThirdStage()
        {
            foreach (var exam in _thirdStageExams)
                exam.UserAction = "";
        }

        public void TakePlombsAndCheckScrewDriverAction()
        {
            if (_secondStageModel.IsTakedScrewdriver)
                _isTakedScrewDriverAfterTakedInstument = true;
        }

        public void EnterInLastPhaset()
        {
            _isSecondPhase = false;
        }

        public void DresUpGloves()
        {
            _isTakedGloves = true;
        }

        public void TakeOffGloves()
        {
            _isTakedGloves = false;
        }

        public void CloseInstruments()
        {
            _parmaService.Close();
            _cE602MService.Close();
            _gerconService.Close();
            _electricClampService.Close();
        }

        private void UseScrewdriver()
        {
            if (_cableScrewPicker.TryPickScrew(out Screw screw) && _secondStageModel.IsTakedScrewdriver
                && _secondStageState.StageState == StageState.Dismantling)
            {
                if (screw.IsOpen)
                {
                    screw.Close();
                }
                else
                {
                    IsFirstPickedScrewDriver = true;
                    screw.Open();
                }
                return;
            }
        }

        public void Update()
        {
            _capsService.TrySetupCap();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_firstStageItemPreview.SelectedItem != null)
                {
                    if(_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Parma_VAF)
                    {
                        PrepareInstruments();
                        _parmaService.Close();
                        _inventoryPanel.Open();
                    }
                    else if (_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.CE602M && !_ce602CanvasObject.activeSelf)
                    {
                        PrepareInstruments();
                        _cE602MService.Close();
                        _inventoryPanel.Open();
                    }
                    else if (_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Clamp_Meters)
                    {
                        PrepareInstruments();
                        _electricClampService.Close();
                    }
                    else if (_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Magnet)
                    {
                        PrepareInstruments();
                        _gerconService.Close();
                    }
                    else if (_firstStageItemPreview.SelectedItem.ItemType == ItemTypes.Stopwatch)
                    {
                        PrepareInstruments();
                        _watchService.Close();

                    }
                    else if(!_ce602CanvasObject.activeSelf)
                    {
                        PrepareInstruments();
                        _itemPreviewPanel.Close();
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (_cE602MItemsPicker.TryPickMagnitePoint(out MagnitePoint magnitePoint))
                {
                    magnitePoint.UnmagniteMovebleObject();
                }
            }

            if (_secondStageState.StageState == StageState.Dismantling)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_cableScrewPicker.TryPickCable(out Cable cable) && _secondStageModel.IsTakedScrewdriver)
                    {
                        cable.PullOutCable();
                        var pulOutsCables = _cables.Where(item => item.IsPullOut).ToList();
                        if(pulOutsCables.Count == _cables.Length)
                        {
                            _secondStagePanel.OnDismantlingPUButton();
                        }
                        return;
                    }
                }
            }
            //if (Input.GetMouseButtonDown(0) &&Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
            //{
            //    Debug.Log(hit.transform);
            //}

            if (Input.GetMouseButtonDown(0))
                UseScrewdriver();

            if (_gameState.CurrentState == State.Preview)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if(_firstStageItemPicker.TryPickScrewdriver(out Screwdriver screwdriver))
                    {
                        screwdriver.gameObject.SetActive(false);
                        _secondStageModel.TakeScrewDriver(screwdriver.gameObject);
                    }
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (_doorPicker.TryPickDoor(out Door door) && !TryPickUI())
                {
                    IsOpenDoors = true;
                    door.Open();
                    return;
                }
                if (_cableScrewPicker.TryPickIKK())
                {
                    if (!_isSecondPhase)
                        _iKK.Close();
                    else
                        _iKK.Open();
                    return;
                }
                if (_cableScrewPicker.TryPickCounterLid())
                {
                    if (!_isSecondPhase)
                        _counterView.CloseLid();
                    else
                        _counterView.OpenLid();
                    return;
                }

            }

            if (_gameState.CurrentState == State.Inventory)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    if (_firstStageItemPicker.TryPickInventoryItem(out InventoryItem inventoryItem))
                    {
                        if (inventoryItem.Item.IsUnpicable)
                            return;
                        if (inventoryItem.Item.ItemType == ItemTypes.USPDBlock)
                        {
                            return;
                        }
                        if (inventoryItem.Item.ItemType == ItemTypes.PowerBlock)
                        {
                            return;

                        }
                        if (inventoryItem.Item.ItemType == ItemTypes.CableForCounter ||
                            inventoryItem.Item.ItemType == ItemTypes.CableForLaptop)
                        {
                            return;
                        }
                        if (inventoryItem.Item.ItemType == ItemTypes.Cables)
                        {
                            return;
                        }
                        if (inventoryItem.Item.ItemType == ItemTypes.Antenna)
                        {
                            return;
                        }
                        if (inventoryItem.Item.ItemType == ItemTypes.SIM_1 ||
                            inventoryItem.Item.ItemType == ItemTypes.SIM_2)
                        {
                            return;
                        }

                        if (inventoryItem.Item.ItemType == ItemTypes.Parma_VAF)
                        {
                            _cE602MService.PrepareToOpen();
                            _gerconService.PrepareToOpen();
                            _electricClampService.PrepareToOpen();
                            _parmaService.Switch(inventoryItem.Item);
                            if (_secondStageModel.IsTakedScrewdriver)
                                _isTakedScrewDriverAfterTakedInstument = true;
                            _isOpenItem = false;
                            IsTakedParmaOrCE = true;

                        }
                        else if (inventoryItem.Item.ItemType == ItemTypes.CE602M)
                        {
                            _parmaService.PrepareToOpen();
                            _gerconService.PrepareToOpen();
                            _electricClampService.PrepareToOpen();
                            _cE602MService.Switch(inventoryItem.Item);
                            if (_secondStageModel.IsTakedScrewdriver)
                                _isTakedScrewDriverAfterTakedInstument = true;
                            _isOpenItem = false;
                            IsTakedParmaOrCE = true;
                        }
                        else if (inventoryItem.Item.ItemType == ItemTypes.Clamp_Meters)
                        {
                            _parmaService.PrepareToOpen();
                            _cE602MService.PrepareToOpen();
                            _gerconService.PrepareToOpen();
                            _electricClampService.Switch(inventoryItem.Item);
                            if (_secondStageModel.IsTakedScrewdriver)
                                _isTakedScrewDriverAfterTakedInstument = true;
                            _isOpenItem = false;
                        }
                        else if (inventoryItem.Item.ItemType == ItemTypes.Plakat)
                        {
                            PrepareInstruments();
                            _plakat.Switch();
                            _itemPreviewPanel.CloseInventoryPanel();
                            _secondStagePanel.Open();
                            _firstStageItemPreview.SelectItemInstrument(inventoryItem.Item);
                            _isOpenItem = false;
                        }
                        else if (inventoryItem.Item.ItemType == ItemTypes.Magnet)
                        {
                            _parmaService.PrepareToOpen();
                            _cE602MService.PrepareToOpen();
                            _electricClampService.PrepareToOpen();
                            _gerconService.Switch(inventoryItem.Item);
                            _itemPreviewPanel.CloseInventoryPanel();
                            _secondStagePanel.Open();
                            _firstStageItemPreview.SelectItemInstrument(inventoryItem.Item);
                            if (_secondStageModel.IsTakedScrewdriver)
                                _isTakedScrewDriverAfterTakedInstument = true;
                            _isOpenItem = false;
                        }
                        else if (inventoryItem.Item.ItemType == ItemTypes.Caps)
                        {
                            PrepareInstruments();
                            _secondStageModel.TakeCaps();
                            _secondStagePanel.Open();
                            _itemPreviewPanel.CloseInventoryPanel();
                            if (_secondStageModel.IsTakedScrewdriver)
                                _isTakedScrewDriverAfterTakedInstument = true;
                            _isOpenItem = false;
                        }
                        else if (inventoryItem.Item.ItemType == ItemTypes.Plombs)
                        {
                        }else if(inventoryItem.Item.ItemType == ItemTypes.Stopwatch)
                        {
                            _watchService.Swtich(inventoryItem.Item);
                            _itemPreviewPanel.CloseInventoryPanel();
                            _secondStagePanel.Open();
                            _firstStageItemPreview.SelectItemInstrument(inventoryItem.Item);
                            _isOpenItem = false;
                        }
                        else
                        {
                            if (_isOpenItem)
                            {
                                PrepareInstruments();
                                _cE602MInstrument.Close();
                                _parmaInstrument.Close();
                                _electricClampInstruments.Close();
                                _firstStageItemPreview.PreviewItem(inventoryItem.Item);
                                _gameState.EnterInPreviewState();
                                _itemPreviewPanel.Close();
                                _isOpenItem = false;
                            }
                            else
                            {
                                PrepareInstruments();
                                _cE602MInstrument.Close();
                                _parmaInstrument.Close();
                                _electricClampInstruments.Close();
                                _firstStageItemPreview.PreviewItem(inventoryItem.Item);
                                _gameState.EnterInPreviewState();
                                _itemPreviewPanel.Open();
                                _isOpenItem = true;
                            }
                        }
                    }
                }
            }
        }

        public bool TryPickUI()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            foreach (var item in results)
            {
                if (item.gameObject.TryGetComponent(out RectTransform rectTransform))
                {
                    return true;
                }
            }
            
            return false;

        }

        public void CloseItemPreview()
        {
            PrepareInstruments();
            _isOpenItem = false;
        }
    }
}