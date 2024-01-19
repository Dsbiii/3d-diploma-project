using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class ObjectTypeItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ObjectTypeItemsPanels _objectTypeItemsPanels;
        private bool _isSelected;


        public string Text => _text.text;
        public bool IsOn => _toggle.isOn;
        private void Awake()
        {
            _toggle.onValueChanged.AddListener(CheckToggle);
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
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isSelected)
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
            _background.color = _selectColor;
            _isSelected = true;
            _objectTypeItemsPanels.Select(this);
        }
        public void Unselect()
        {
            _toggle.SetIsOnWithoutNotify(false);
            _background.color = _baseColor;
            _isSelected = false;
        }
    }
}