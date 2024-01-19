using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class MeteringDevicesObject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Color _selectColor;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Image _image;
        [SerializeField] private MEteringDevicesService _service;
        [SerializeField] private TMP_Text _name;
        private bool _isActive;
        public string Name { get { return _name.text; } }
        public bool IsSelected { get { return _isActive; } }
        private void Awake()
        {
            _toggle.onValueChanged.AddListener(CheckToggle);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isActive)
            {
                Select();
            }
            else
            {
                Unselect();
            }
        }
        private void Select()
        {
            _toggle.SetIsOnWithoutNotify(true);
            _service.SetDeviceName(_name.text);
            _image.color = _selectColor;
            _isActive = true;
            _service.Selected(this);
        }
        public void Unselect()
        {
            _toggle.SetIsOnWithoutNotify(false);
            _image.color = _baseColor;
            _isActive = false;
        }
        private void CheckToggle(bool toggle)
        {
            if(toggle)
            {
                Select();
            }
            else
            {
                Unselect();
            }
        }
    }
}