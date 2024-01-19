using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class RouteEquipment : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Image _image;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private TMP_Text _type;
        [SerializeField] private TMP_Text _priority;
        [SerializeField] private TMP_Text _port;
        private string _ipAdress;
        private RouteEquimentPanel _panel;
        private Button _edit;
        private Button _delete;

        public bool IsSelected { get; private set; } = false;
        private void Awake()
        {
            _toggle.onValueChanged.AddListener(CheckToggle);
        }
        public void Init(string type, string priority, Button edit, Button delete, RouteEquimentPanel panel)
        {
            _type.text = type;
            _priority.text = priority;
            _edit = edit;
            _delete = delete;
            _panel = panel;
            _edit.onClick.AddListener(Edit);
            _delete.onClick.AddListener(Delete);
        }
        public void SetValue(string port, string IpAdress)
        {
            _port.text = port;
            _ipAdress = IpAdress;
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
            }
        }

        public void Select()
        {
            _toggle.SetIsOnWithoutNotify(true);
            _edit.interactable = true;
            _delete.interactable = true;
            _image.color = _selectColor;
            IsSelected = true;
            _panel.Selected(this);
        }

        public void Unselect()
        {
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
                _edit.interactable = false;
                _delete.interactable = false;
            }
        }
        private void Edit()
        {
            _panel.Edit(this, _port.text, _ipAdress);
        }
        private void Delete()
        {
            _panel.Delete(this);
        }
    }
}