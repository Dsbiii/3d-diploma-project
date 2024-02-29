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
            _panel.SetActive(true);
        }
        private void Unselect()
        {
            _toggle.SetIsOnWithoutNotify(false);
            _isSelect = false;
            _image.color = _baseColor;
            _panel.SetActive(false);
        }
        private void OnEnable()
        {
            Unselect();
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