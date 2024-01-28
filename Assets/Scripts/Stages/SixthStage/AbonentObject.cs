using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class AbonentObject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private Button _editButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _nameValue;
        [SerializeField] private TMP_Text _serialNumberValue;
        [SerializeField] private TMP_Text _surnameValue;
        [SerializeField] private TMP_Text _lastNameValue;
        [SerializeField] private TMP_Text _typeValue;
        private int _value;
        private AbonentPanel _Service;
        public string Surname => _surnameValue.text;
        public string Name => _nameValue.text;
        public string SerialNumber => _serialNumberValue.text;
        public string LastName => _lastNameValue.text;
        bool _isActive = false;
        private void Awake()
        {
            _toggle.onValueChanged.AddListener(CheckToggle);
        }
        public void Init(string name, string serialNumber, string surname, string lastName, string type, Button edit, Button delete, AbonentPanel abonentPanel, int value)
        {
            _Service = abonentPanel;
            _editButton = edit;
            _deleteButton = delete;
            _nameValue.text = name;
            _serialNumberValue.text = serialNumber;
            _typeValue.text = type;
            _surnameValue.text = surname;
            _lastNameValue.text = lastName;
            _value = value;
            _editButton.onClick.AddListener(Edit);
            _deleteButton.onClick.AddListener(Delete);
        }
        public void SetValue(string name, string serialNumber, string surname, string lastName, string type, int value)
        {
            _nameValue.text = name;
            _serialNumberValue.text = serialNumber;
            _typeValue.text = type;
            _surnameValue.text = surname;
            _lastNameValue.text = lastName;
            _value = value;
        }
        public void OnPointerClick(PointerEventData eventData)
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
            _isActive = true;
            _Service.SelectedObject(this); ;
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
        private void Edit()
        {
            if(_toggle.isOn)
                _Service.Edit(_nameValue.text, _serialNumberValue.text, _surnameValue.text, _lastNameValue.text, _typeValue.text, this, _value);
        }
        private void Delete()
        {
            if (_toggle.isOn)
            {
                _Service.Delete(this);
            }
        }
    }
}