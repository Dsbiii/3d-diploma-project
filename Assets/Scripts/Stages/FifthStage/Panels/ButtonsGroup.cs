using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class ButtonsGroup : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private List<ButtonsGroup> _buttons;
        public bool Selected { get; private set; }

        public System.Action OnClick;

        public void Select()
        {
            foreach(var button in _buttons)
            {
                button.UnselectColor();
            }
            SelectColor();
        }

        public void SelectColor()
        {
            OnClick?.Invoke();
            Selected = true;
            _image.color = _selectedColor;  
        }

        public void UnselectColor()
        {
            Selected = false;
            _image.color = _baseColor;
        }

    }
}