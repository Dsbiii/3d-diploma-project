using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class SelectPanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private Button _editButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _location;
        [SerializeField] private TMP_Text _serialNumber;
        [SerializeField] private TMP_Text _date;
        private string _dateSetup;
        private string _dateNextCheck;
        private string _dateLastCheck;
        private string _user;
        private string _password;
        private string _time;
        private string _connectNumber;
        private int _timeZoneValue;
        private int _locationValue;
        public string Name { get { return _name.text; } }
        public string Location { get { return _location.text; } }
        public string SerialNumber { get { return _serialNumber.text; } }
        public string Date { get { return _date.text; } }

        [SerializeField] private ObjectService _service;

        public bool _isActive = false;
        private void Awake()
        {
            if (_toggle != null)
                _toggle.onValueChanged.AddListener(CheckToggle);
        }
        public void Init(Button editButton, Button deleteButton, string name, string location, string serialNumber, string date,
            ObjectService objectService, int value, string dateSetup, string dateNextCheck, string dateLastCheck, string user,
            string password, string time, string connectNumber, int timeZoneValue)
        {
            _editButton = editButton;
            _deleteButton = deleteButton;
            _name.text = name;
            _location.text = location;
            _serialNumber.text = serialNumber;
            _date.text = date;
            _service = objectService;
            _locationValue = value;
            _time = time;
            _connectNumber = connectNumber;
            _timeZoneValue = timeZoneValue;
            _dateLastCheck = dateLastCheck;
            _user = user;
            _password = password;
            _dateNextCheck = dateNextCheck;
            _dateSetup = dateSetup;
            _deleteButton.onClick.AddListener(Delete);
            _editButton.onClick.AddListener(Edit);
            Debug.Log("Point " + GetPoint());
        }
        public void SetValue(string name, string location, string serialNumber, string date, int value, string dateSetup, string dateNextCheck, string dateLastCheck, string user,
            string password, string time, string connectNumber, int timeZoneValue)
        {
            _name.text = name;
            _location.text = location;
            _serialNumber.text = serialNumber;
            _date.text = date;
            _locationValue = value;
            _time = time;
            _connectNumber = connectNumber;
            _timeZoneValue = timeZoneValue;
            _dateLastCheck = dateLastCheck;
            _user = user;
            _password = password;
            _dateNextCheck = dateNextCheck;
            _dateSetup = dateSetup;
        }
        public void SetValue(string name, string location, string serialNumber, string date)
        {
            _name.text = name;
            _location.text = location;
            _serialNumber.text = serialNumber;
            _date.text = date;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (Input.GetMouseButtonUp(1))
            {
                Debug.Log("_isActive" + _isActive);
                if (_isActive)
                {
                    _service.CreateContextMenu();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (!_isActive)
                {
                    Select();
                }
                else
                {
                    if (_button != null)
                        _button.interactable = false;
                    if (_deleteButton != null)
                        _deleteButton.interactable = false;
                    if (_editButton != null)
                        _editButton.interactable = false;
                    Unselect();
                }
            }
        }
        private void Select()
        {
            if (_button != null)
                _button.interactable = true;
            if (_deleteButton != null)
                _deleteButton.interactable = true;
            if (_editButton != null)
                _editButton.interactable = true;
            if (_toggle != null)
                _toggle.SetIsOnWithoutNotify(true);
            _image.color = _selectColor;
            if (_service != null)
                _service.SelectedObject(this);
            _isActive = true;
        }
        public void Unselect()
        {
            if (_toggle != null)
                _toggle.SetIsOnWithoutNotify(false);
            _image.color = _baseColor;
            _isActive = false;
        }
        private void CheckToggle(bool toggle)
        {
            if (toggle)
                Select();
            else
                Unselect();
        }
        private void Delete()
        {
            if (_toggle.isOn)
                _service.Delete(this);
        }
        private void Edit()
        {
            if (_toggle.isOn)
                _service.Edit(_name.text, _location.text, _serialNumber.text, _date.text, _locationValue, this, _dateSetup, _dateNextCheck, _dateLastCheck, _user, _password,
                    _time, _connectNumber, _timeZoneValue);
        }
        public int GetPoint()
        {
            int Point = 1;
            Point += CheckConnectNumber() + CheckCounter() + CheckDates() + CheckSerialNumber();
            Debug.Log(CheckCounter() + " " + CheckSerialNumber() + " " + CheckDates() + " " + CheckConnectNumber());
            return Point;
        }
        private int CheckCounter()
        {
            if (_name.text == "СЭТ-4ТМ.03М")
                return 1;
            return 0;
        }
        private int CheckSerialNumber()
        {
            if (_serialNumber.text == "0112055629")
                return 1;
            return 0;
        }
        private int CheckConnectNumber()
        {
            if (_connectNumber == "+79273266002")
                return 1;
            return 0;
        }
        private int CheckDates()
        {
            int Point = 0;
            if (_date.text != "")
                Point++;
            if (_dateLastCheck != "")
                Point++;
            if (_dateNextCheck != "")
                Point++;
            if (_dateSetup != "")
                Point++;
            return Point;
        }
    }
}