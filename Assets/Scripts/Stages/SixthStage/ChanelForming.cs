using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class ChanelForming : MonoBehaviour , IPointerClickHandler
    {
        [SerializeField] private List<MeteringDevicesObject> _selectObjects;
        [SerializeField] private bool _isSecondValue;
        [SerializeField] private List<ChanelFormingTool> _chanelFromingTool;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _type;
        [SerializeField] private TMP_Text _priority;
        [SerializeField] private TMP_Text _port;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private Button _editButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Image _image;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectedColor;
         
        private ChanelFormingTool _chanelFormingTool;

        public void SetupValues()
        {
            if (_isSecondValue)
            {
                _port.text = "Подключение к SM160, №01994 (включен)";
            }
            else
            {
                _port.text = "Подключение к " + _valueText.text;
            }
            foreach (var item in _selectObjects)
            {
                if (item.IsSelected)
                    _name.text = item.Name;
            }
        }

        public void Create()
        {
            foreach (var item in _selectObjects)
            {
                if (item.IsSelected)
                    _name.text = item.Name;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _editButton.interactable = true;
            _deleteButton.interactable = true;
            _image.color = _selectedColor;
        }
        public void SetValue(string type, string priority)
        {
            _type.text = type;
            _priority.text = priority;
        }

    }
}