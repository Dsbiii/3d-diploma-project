using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class SelectObject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Image _image;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private TMP_Text _state;
        [SerializeField] private TMP_Text _login;
        [SerializeField] private TMP_Text _abonent;
        [SerializeField] private TMP_Text _profileAbonent;
        [SerializeField] private Button _edit;
        [SerializeField] private Button _delete;
        [SerializeField] private Button _setPassword;
        [SerializeField] private AbonentCreatePanel _createPanel;

        public string Value => _value.text;
        public bool IsSelected { get; private set; } = false;
        private void Awake()
        {
            if (_toggle != null)
                _toggle.onValueChanged.AddListener(CheckToggle);
            if (_edit != null)
                _edit.onClick.AddListener(Edit);
            if (_delete != null)
                _delete.onClick.AddListener(Delete);
        }
        public void Init(string login, string abonent, string profileAbonent, Button edit, Button delete, Button setPassword, AbonentCreatePanel createPanel)
        {
            _login.text = login;
            if (abonent == "Выбрать...")
                _abonent.text = "";
            else
                _abonent.text = abonent;
            if (profileAbonent == "Выбрать...")
                _profileAbonent.text = "";
            else
                _profileAbonent.text = profileAbonent;
            _edit = edit;
            _delete = delete;
            _setPassword = setPassword;
            _createPanel = createPanel;
            _edit.onClick.AddListener(Edit);
            _delete.onClick.AddListener(Delete);
        }
        public void SetValue(string login, string abonent, string abonentProfile)
        {
            _login.text = login;
            _abonent.text = abonent;
            _profileAbonent.text = abonentProfile;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsSelected)
                Select();
            else
            {
                Unselect();
                if (_edit != null)
                    _edit.interactable = false;
                if (_delete != null)
                    _delete.interactable = false;
                if (_setPassword != null)
                    _setPassword.interactable = false;
            }
        }

        public void Select()
        {
            if (_toggle != null)
                _toggle.SetIsOnWithoutNotify(true);
            if (_edit != null)
                _edit.interactable = true;
            if (_delete != null)
                _delete.interactable = true;
            if (_setPassword != null)
                _setPassword.interactable = true;
            _image.color = _selectColor;
            if (_createPanel != null)
                _createPanel.Select(this);
            IsSelected = true;
        }

        public void Unselect()
        {
            if (_toggle != null)
                _toggle.SetIsOnWithoutNotify(false);
            _image.color = _baseColor;
            IsSelected = false;
        }
        private void CheckToggle(bool state)
        {
            if (state)
            {
                Select();
            }
            else
            {
                Unselect();
                if (_edit != null)
                    _edit.interactable = false;
                if (_delete != null)
                    _delete.interactable = false;
            }
        }
        private void Edit()
        {
            Debug.Log("Stid");
            if (IsSelected)
                _createPanel.Edit(_login.text, _abonent.text, _profileAbonent.text);
        }
        private void Delete()
        {
            if (IsSelected)
                _createPanel.Delete(this);
        }
        public int Points()
        {
            int point = 0;
            if (_login.text != "")
                point =+3;
            return point;
        }
    }
}