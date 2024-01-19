using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Assets.Scripts.Stages.FourthStage.SelectingCablesPanel
{
    public class FourthStageCableOption : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
    {
        public event System.Action<FourthStageCableOption> OnClicked;
        [SerializeField] private bool _isRight;
        [SerializeField] private Sprite _select;
        [SerializeField] private Sprite _hoverEnter;
        [SerializeField] private Sprite _hoverLeft;
        private Image _image;

        public bool IsSelect { get; private set; }

        public bool IsRight => _isRight;


        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this);
            if (!IsSelect)
            {
                _image.sprite = _select;
                IsSelect = true;
            }
            else
            {
                _image.sprite = _hoverEnter;
                IsSelect = false;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!IsSelect)
                _image.sprite = _hoverEnter;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!IsSelect)
                _image.sprite = _hoverLeft;
        }
    }
}