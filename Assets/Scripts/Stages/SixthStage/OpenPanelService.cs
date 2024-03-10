using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class OpenPanelService : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Image _image;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject _panelData;
        private bool _isData;
        private bool _isSelect;
        private void Awake()
        {
            _toggle.onValueChanged.AddListener(CheckToggle);
        }
        private void CheckToggle(bool toggle)
        {
            if (toggle)
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
            _isSelect = true;
            _image.color = _selectColor;
            if(! _isData )
                _panel.SetActive(true);
            else
                _panelData.SetActive(true);
        }
        private void Unselect()
        {
            _toggle.SetIsOnWithoutNotify(false);
            _isSelect = false;
            _image.color = _baseColor;
            if (!_isData)
                _panel.SetActive(false);
            else 
                _panelData.SetActive(false);
        }
        private void OnEnable()
        {
            if (_panelData.activeSelf)
            {
                _isData = true;
                Select();
                return;
            }
            Unselect();
        }
        private void OnDisable()
        {
            _isData = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_isSelect)
            {
                Unselect();
            }
            else
            {
                Select();
            }
        }
    }
}