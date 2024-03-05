using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class DataCollectorObject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Image _image;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _setting;
        [SerializeField] private TMP_Text _profileSetting;
        [SerializeField] private Toggle _stateToggle;
        [SerializeField] private Button _edit;
        [SerializeField] private Button _delete;
        [SerializeField] private Button _schedule;
        [SerializeField] private DataCollectorService _service;
        [SerializeField] private bool _dontCheck;
        private string _collectionDepthDay;
        private string _collectionDepthHour;
        private string _collectionDepthMin;
        private string _collectionDepthSec;
        private string _stopWorkDay;
        private string _stopWorkHour;
        private string _stopWorkMin;
        private string _stopWorkSec;
        private bool _disableToggle;
        private bool _checkDateBaseToggle;
        private bool _loadStatusToggle;
        private bool _tarifToggle;
        private bool _configToggle;
        private bool _statusIOToggle;
        private bool _startAfterServiceToggle;
        public bool IsSelected { private set; get; } = false;
        public string Name => _name.text;
        public void Init(string name, string setting, string profileSetting, bool state, Button edit, Button delete, Button schedule, DataCollectorService collectorService,
            string CDDay, string CDHour, string CDMin, string CDSec, string SWDay, string SWHour, string SWMin, string SWSec,
            bool disable, bool checkDB, bool loadStatus, bool tarif, bool config, bool statusIO, bool startAfterService)
        {
            _name.text = name;
            _setting.text = setting;
            _profileSetting.text = profileSetting;
            _stateToggle.isOn = state;
            _edit = edit;
            _delete = delete;
            _schedule = schedule;
            _service = collectorService;
            _collectionDepthDay = CDDay;
            _collectionDepthHour = CDHour;
            _collectionDepthMin = CDMin;
            _collectionDepthSec = CDSec;
            _stopWorkDay = SWDay;
            _stopWorkHour = SWHour;
            _stopWorkMin = SWMin;
            _stopWorkSec = SWSec;
            _disableToggle = disable;
            _checkDateBaseToggle = checkDB;
            _loadStatusToggle = loadStatus;
            _tarifToggle = tarif;
            _configToggle = config;
            _statusIOToggle = statusIO;
            _startAfterServiceToggle = startAfterService;
            _edit.onClick.AddListener(Edit);
            _delete.onClick.AddListener(Delete);
            _schedule.onClick.AddListener(Schedule);
        }
        public void SetValue(string name, string setting, string profileSetting, bool state, string CDDay, string CDHour, string CDMin, string CDSec, string SWDay, string SWHour, string SWMin, string SWSec,
            bool disable, bool checkDB, bool loadStatus, bool tarif, bool config, bool statusIO, bool startAfterService)
        {
            _name.text = name;
            _setting.text = setting;
            _profileSetting.text = profileSetting;
            _stateToggle.isOn = state;
            _collectionDepthDay = CDDay;
            _collectionDepthHour = CDHour;
            _collectionDepthMin = CDMin;
            _collectionDepthSec = CDSec;
            _stopWorkDay = SWDay;
            _stopWorkHour = SWHour;
            _stopWorkMin = SWMin;
            _stopWorkSec = SWSec;
            _disableToggle = disable;
            _checkDateBaseToggle = checkDB;
            _loadStatusToggle = loadStatus;
            _tarifToggle = tarif;
            _configToggle = config;
            _statusIOToggle = statusIO;
            _startAfterServiceToggle = startAfterService;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsSelected)
                Select();
            else
            {
                Unselect();
                _edit.interactable = false;
                _delete.interactable = false;
                _schedule.interactable = false;
            }
        }

        private void Awake()
        {
            _toggle.onValueChanged.AddListener(CheckToggle);
        }
        private void CheckToggle(bool toggle)
        {
            if (toggle)
                Select();
            else
            {
                Unselect();
                _edit.interactable = false;
                _delete.interactable = false;
                _schedule.interactable = false;
            }
        }
        private void Select()
        {
            _edit.interactable = true;
            _delete.interactable = true;
            _schedule.interactable = true;
            _service.SelectedObject(this);
            _toggle.SetIsOnWithoutNotify(true);
            _image.color = _selectColor;
            IsSelected = true;
        }
        public void Unselect()
        {
            _toggle.SetIsOnWithoutNotify(false);
            _image.color = _baseColor;
            IsSelected = false;
        }
        private void Edit()
        {
            if (IsSelected)
                _service.Edit(_name.text, _setting.text, _profileSetting.text, _stateToggle.isOn, this,
                    _collectionDepthDay, _collectionDepthHour, _collectionDepthMin, _collectionDepthSec, _stopWorkDay, _stopWorkHour,
                    _stopWorkMin, _stopWorkSec, _disableToggle, _checkDateBaseToggle, _loadStatusToggle, _tarifToggle, _configToggle,
                    _statusIOToggle, _startAfterServiceToggle);
        }
        private void Delete()
        {
            if (IsSelected)
                _service.Delete(this);
        }
        private void Schedule()
        {
            if (IsSelected)
                _service.SetTitle(_name.text);
        }
        public int Points()
        {
            if(_dontCheck) 
                return 0;
            int Points = 0;
            if (_name.text != "")
            {
                _service.Report[0] = 1;
                Points++;
            }
            if (_collectionDepthDay != "" || _collectionDepthHour != "" || _collectionDepthMin != "" || _collectionDepthSec != "")
            {
                _service.Report[1] = 1;
                Points++;
            }
            if (_stateToggle.isOn && _checkDateBaseToggle && _startAfterServiceToggle)
            {
                _service.Report[3] = 1;
                Points += 2;
            }
            if (_setting.text == "Фактическая активная мощность за 1 час, кВт")
            {
                _service.Report[2] = 1;
                Points++;
            }
            if (_stopWorkDay != "" || _stopWorkHour != "" || _stopWorkMin != "" || _stopWorkSec != "")
            {
                _service.Report[4] = 1;
                Points += 2;
            }
            return Points;
        }
    }
}