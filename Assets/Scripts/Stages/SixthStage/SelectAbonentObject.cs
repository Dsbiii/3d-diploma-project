using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class SelectAbonentObject : MonoBehaviour , IPointerClickHandler
    {
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private SelectAbonentPanel _selectObjects;
        [SerializeField] private Toggle _toggle;

        public string Value => _text.text;
        public bool IsSelected { get; private set; }
        private void Start()
        {
            if(_toggle != null)
                _toggle.onValueChanged.AddListener(CheckToggleState);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
        }
        public void Init(string text, SelectAbonentPanel selectObjects)
        {
            _text.text = text;
            _selectObjects = selectObjects;
        }

        public void Select()
        {
            foreach (var obj in _selectObjects.SelectAbonentObjects)
            {
                if(obj != this)
                    obj.Unselect();
            }
            if (_toggle != null)
                _toggle.isOn = true;
            _image.color = _selectColor;
            IsSelected = true;
        }

        public void Unselect()
        {
            if (_toggle != null)
                _toggle.isOn = false;
            IsSelected = false;
            _image.color = _baseColor;
        }
        public void CheckToggleState(bool state)
        {
            if (state)
                Select();
            else
                Unselect();

        }
    }
}