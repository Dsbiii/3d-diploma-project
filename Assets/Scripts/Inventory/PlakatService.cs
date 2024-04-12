using Assets.Scripts.Instruments;
using Assets.Scripts.Stages.FourthStage;
using Assets.Scripts.Stages.SecondStage;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlakatService : MonoBehaviour
{
    [SerializeField] private AktReport _aktReport;
    [SerializeField] private List<FourthStagePlomb> _fourthStagePlombs;
    [SerializeField] private Plakat[] _plakatsItems;
    [SerializeField] private LayerMask _plakatLayerMask;
    [SerializeField] private SecondStageController _secondStageController;
    [SerializeField] private GameObject[] _plakatsList;
    [SerializeField] private GameObject _plakats;
    [SerializeField] private GameObject[] _panelsExeptions;
    [Inject] private FourthStageModel _fourthStageModel;
    private ParmaService _parmaService;
    private CE602MService _cE602MService;

    private bool _isSetupPlakatsBeforeScrewDriver = true;
    private bool _isSetupPlakatsBeforeUseInstruments = true;

    private bool _isOpen;
    public bool IsSetupedPlombsBeforeExitFromTP = false;
    public bool IsSetupedPlakat { get; private set; }   
    private bool _isRightSetupedPlakat;

    public void InitFromController(ParmaService parmaService, CE602MService cE602MService)
    {
        _parmaService = parmaService;
        _cE602MService = cE602MService;
    }

    public void PreparePlakatsBeforeExitFromTP()
    {
        _fourthStagePlombs = _fourthStagePlombs.Where(item => !item.IsSetupedPlomb).ToList();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _panelsExeptions.Where(item => !item.activeSelf).ToArray().Length == _panelsExeptions.Length && _isOpen)
        {
            TryPickPlakat();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _isOpen)
        {
            _plakats.SetActive(false);
            _isOpen = false;
        }
    }

    public void ResetPlakats()
    {
        foreach (var item in _plakatsList)
        {
            item.GetComponent<PlakatPack>().OffActivePlakats();
        }
        foreach (var item in _plakatsItems)
        {
            item.ResetPlakat();
        }
        _plakats.SetActive(false);
        _isOpen = false;
    }

    public void TryPickPlakat()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform != null && hit.transform.CompareTag("Placat"))
            {
                if (hit.transform.TryGetComponent(out PlakatPack plakat))
                {
                    plakat.TryIncreaseCurrentPlakat();
                    plakat.OffActivePlakats();
                }

                if (hit.transform.childCount > 4)
                {
                    var item = hit.transform.GetChild(4);
                    item.gameObject.SetActive(true);
                    //item.SetParent(_plakats.transform);
                    //item.position = new Vector3(0, 0, 0);
                    //for (int i = 0; i < hit.transform.childCount; i++)
                    //{
                    //    hit.transform.GetChild(i).gameObject.SetActive(false);
                    //}
                }
            }
        }
    }

    public void SetupedPlakat()
    {
        IsSetupedPlakat = true;
        if (_fourthStageModel.IsExitedFromTP && !IsSetupedPlombsBeforeExitFromTP
            && !_aktReport.IsOpenedBeforeExitFromTP)
        {
            _isRightSetupedPlakat = true;
        }
    }

    public void SetSetupPlakatsBeforeScrewDriver()
    {
        if (_secondStageController.IsFirstPickedScrewDriver)
            _isSetupPlakatsBeforeScrewDriver = false;
        if (_cE602MService.IsWasTaked && _parmaService.IsWasTaked)
            _isSetupPlakatsBeforeUseInstruments = false;
    }

    public void ReportWrongPlakatsForSecondStage()
    {
        // _secondStageController.AddSecondStageExam("Установка плакатов", "Ошибка", "Открыть двери ТП и правильно обозначить плакатами зоны работ и Высокого напряжения. Не используемые плакаты убираются в сумку", 0, 0);
    }

    public void ReportWrongPlakatsForThirdStage()
    {
        // _secondStageController.AddThirdStageExam("Установка плакатов", "Ошибка", "Открыть двери ТП и правильно обозначить плакатами зоны работ и Высокого напряжения. Не используемые плакаты убираются в сумку", 0, 0);
    }


    public void ReportPlakatsForSecondStage()
    {
        //_secondStageController.AddSecondStageExam("Установка плакатов", "Правильно", "Открыть двери ТП и правильно обозначить плакатами зоны работ и Высокого напряжения. Не используемые плакаты убираются в сумку", 1, 0);

        if (_isRightSetupedPlakat && !_isOpen && _plakatsList[0].transform.GetChild(2).gameObject.activeSelf == true && _plakatsList[1].transform.GetChild(2).gameObject.activeSelf == true && _plakatsList[2].transform.GetChild(3).gameObject.activeSelf == true)
        {
            _secondStageController.AddSecondStageExam("Установка плакатов", "Правильно", "Открыть двери ТП и правильно обозначить плакатами зоны работ и Высокого напряжения. Не используемые плакаты убираются в сумку", 1, 0);
        }
        else
        {
            _secondStageController.AddSecondStageExam("Установка плакатов", "Ошибка", "Открыть двери ТП и правильно обозначить плакатами зоны работ и Высокого напряжения. Не используемые плакаты убираются в сумку", 0, 0);
        }
    }

    public bool CheckForRightFieldPlakats()
    {
        //if (_isRightSetupedPlakat && _plakatsList[0].transform.GetChild(2).gameObject.activeSelf == true && _plakatsList[1].transform.GetChild(2).gameObject.activeSelf == true && _plakatsList[2].transform.GetChild(3).gameObject.activeSelf == true)

        if (_isRightSetupedPlakat && _plakatsList[0].transform.GetChild(2).gameObject.activeSelf == true && _plakatsList[1].transform.GetChild(2).gameObject.activeSelf == true && _plakatsList[2].transform.GetChild(3).gameObject.activeSelf == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReportPlakatsForThirdStage()
    {
        //_secondStageController.AddThirdStageExam("Установка плакатов", "Правильно", "Открыть двери ТП и правильно обозначить плакатами зоны работ и Высокого напряжения. Не используемые плакаты убираются в сумку", 1, 0);

        if (!_isOpen && _plakatsList[0].transform.GetChild(2).gameObject.activeSelf == true && _plakatsList[1].transform.GetChild(2).gameObject.activeSelf == true && _plakatsList[2].transform.GetChild(3).gameObject.activeSelf == true)
        {
            _secondStageController.AddThirdStageExam("Установка плакатов", "Правильно", "Открыть двери ТП и правильно обозначить плакатами зоны работ и Высокого напряжения. Не используемые плакаты убираются в сумку", 1, 0);
        }
        else
        {
            _secondStageController.AddThirdStageExam("Установка плакатов", "Ошибка", "Открыть двери ТП и правильно обозначить плакатами зоны работ и Высокого напряжения. Не используемые плакаты убираются в сумку", 0, 0);

        }
    }


    public void Switch()
    {
        if (_isOpen)
        {
            _plakats.SetActive(false);
            _isOpen = false;
        }
        else
        {
            _plakats.SetActive(true);
            _isOpen = true;
        }
    }

}