using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage.Directories
{
    public class EquipmentObjectType : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _type;
        [SerializeField] private TMP_Text _serialNumber;
        [SerializeField] private TMP_Text _placePoint;
        [SerializeField] private TMP_Text _value;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Image _image;
        [SerializeField] private Toggle _toggle;
        private string _user;
        private string _password;
        private string _timeZone;
        private string _timePoint;
        private int _valuePlacePoint;
        private int _valueTimePoint;
        public int[] Report = new int[5] {1,0,0,0,0};
        public string User { get { return _user; } }
        public string Password { get { return _password; } }
        public string TimeZone { get { return _timeZone; } }
        public string TimePoint { get { return _timePoint; } }
        public int ValuePlacePoint { get { return _valuePlacePoint; } }
        public int ValueTimePoint { get { return _valueTimePoint; } }
        public string Name { get { return _name.text; } }
        public string Type { get { return _type.text; } }
        public string SerialNumber { get { return _serialNumber.text; } }
        public string PlaceNumber { get { return _placePoint.text; } }
        public string Value => _value.text;
        public bool IsSelected { get; private set; }
        public event Action<EquipmentObjectType> OnSelected;
        public event Action<bool> isSelected;
        public bool CriticalError;
        public bool DontSM;
        private void Awake()
        {
            _toggle.onValueChanged.AddListener(CheckToggle);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
        }

        public void Select()
        {
            _toggle.SetIsOnWithoutNotify(true);
            IsSelected = true;
            _image.color = _selectColor;
            OnSelected?.Invoke(this);
            isSelected?.Invoke(true);
        }

        public void Unselect()
        {
            _toggle.SetIsOnWithoutNotify(false);
            IsSelected = false;
            _image.color = _baseColor;
            isSelected?.Invoke(false);
        }

        public void Init(string name, string type, string serialNumber, string placePoint,
            string user, string password, string timeZone, string timePoint, int valuePlacePoint, int valueTimePoint)
        {
            _name.text = name;
            _type.text = type;
            _serialNumber.text = serialNumber;
            _placePoint.text = placePoint;
            _user = user;
            _password = password;
            _timeZone = timeZone;
            _timePoint = timePoint;
            _valuePlacePoint = valuePlacePoint;
            _valueTimePoint = valueTimePoint;
        }

        public void CheckToggle(bool value)
        {
            if (value)
            {
                Select();
            }
            else
            {
                Unselect();
            }
        }
        public int GetPoints()
        {
            int Point = 1;
            Report[0] = 1;
            if (_name.text == "SM160")
            {
                if (CheckSerialNumber())
                {
                    Report[1] = 1;
                    Point++;
                }
                if (CheckUser())
                {
                    Report[2] = 1;
                    Point++;
                }
                if (CheckTimeZone())
                {
                    Report[3] = 1;
                    Point++;
                }
                if (CheckPlacePoint())
                {
                    Report[4] = 1;
                    Point++;
                }
                else
                {
                    Point = 0;
                }
            }
            else
            {
                DontSM = false;
                Point = 0;
            }
            return Point;
        }
        private bool CheckSerialNumber()
        {
            if (_serialNumber.text == "011994")
                return true;
            return false;
        }
        private bool CheckUser()
        {
            if (_user == "admin" && _password == "12345")
                return true;
            return false;
        }
        private bool CheckTimeZone()
        {
            if (_valueTimePoint == 1)
                return true;
            return false;
        }
        public bool CheckPlacePoint()
        {
            if (_valuePlacePoint == 5)
            {
                return true;
            }
            CriticalError = true;
            return false;
        }
    }
}