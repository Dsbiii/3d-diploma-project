using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase
{
    public class CableOption : MonoBehaviour , IPointerClickHandler
    {
        public event System.Action<CableOption> OnClicked;
        [SerializeField] private bool _isRight;
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
                _image.color = Color.green;
                IsSelect = true;
            }
            else
            {
                _image.color = Color.white;
                IsSelect = false;
            }
        }
    }
}