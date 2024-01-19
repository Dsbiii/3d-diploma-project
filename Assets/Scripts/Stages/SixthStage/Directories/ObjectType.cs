using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage.Directories
{
    public class ObjectType : MonoBehaviour , IPointerClickHandler
    {
        [SerializeField] private Image _image;
        private Color _selectColor = Color.blue;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Toggle _toggle;
        private Color _baseColor;

        public string Name => _name.text;

        public event System.Action<ObjectType> OnClick;

        private void Awake()
        {
            _image = GetComponent<Image>(); 
            _baseColor = _image.color;
            if(_toggle  != null)
                _toggle.onValueChanged.AddListener(CheckToggle);
        }

        public void Select()
        {
            if (_toggle != null)
            {
                _toggle.SetIsOnWithoutNotify(true);
            }
            _image.color = _selectColor;
        }

        public void Unselect()
        {
            if(_toggle != null)
            {
                _toggle.SetIsOnWithoutNotify(false);
            }
            _image.color = _baseColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }
        public void CheckToggle(bool toggle)
        {
            if (toggle)
            {
                OnClick?.Invoke(this);
            }
            else
            {
                OnClick?.Invoke(this);
            }
        }
    }
}