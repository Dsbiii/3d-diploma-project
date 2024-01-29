using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class ChanelForming : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private TMP_Text _type;
        [SerializeField] private TMP_Text _priority;
        [SerializeField] private TMP_Text _port;
        [SerializeField] private Image _image;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectedColor;
        private MEteringDevicesService _service;
        private string _name;
        private Button _edit;
        private Button _delete;
        private string _inputField;
        public bool IsSelected { get; private set; }

        private ChanelFormingTool _chanelFormingTool;
        private void Awake()
        {
            _toggle.onValueChanged.AddListener(ToggleCheck);
        }
        private void ToggleCheck(bool value)
        {
            if (value)
            {
                Select();
            }
            else
            {
                Unselect();
                _edit.interactable = false;
                _delete.interactable = false;
            }
        }
        public void SetupValues(string port, string inputfield)
        {
            _port.text = port;
            _inputField = inputfield;
        }
        private void Select()
        {
            _toggle.SetIsOnWithoutNotify(true);
            _edit.interactable = true;
            _delete.interactable = true;
            _image.color = _selectedColor;
            IsSelected = true;
            _service.Selected(this);
        }
        public void Unselect()
        {
            _toggle.SetIsOnWithoutNotify(false);
            IsSelected = false;
            _image.color = _baseColor;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsSelected)
            {
                Select();
            }
            else
            {
                Unselect();
                _edit.interactable = false;
                _delete.interactable = false;
            }
        }
        public void Init(string type, string priority, Button edit, Button delete, MEteringDevicesService devicesService, string name)
        {
            _type.text = type;
            _priority.text = priority;
            _edit = edit;
            _delete = delete;
            _service = devicesService;
            _name = name;
            _edit.onClick.AddListener(Edit);
            _delete.onClick.AddListener(Delete);
        }
        private void Edit()
        {
            if (IsSelected)
                _service.Edit(this);
        }
        private void Delete()
        {
            if (IsSelected)
                _service.Delete(this);
        }
        public int SumPoints()
        {
            int Point = 0;
            Point += CheckPort() + CheckType();
            return Point;
        }
        private int CheckType()
        {
            if (_type.text == "Маршрут через SM160")
                return 1;
            return 0;
        }
        private int CheckPort()
        {
            if (_port.text == "Подключение к SM160, №01994 (включен)")
                return 1;
            return 0;
        }
    }
}