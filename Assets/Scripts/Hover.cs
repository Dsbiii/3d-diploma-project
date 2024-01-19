using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hover : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _hoverSprite;
    [SerializeField] private Sprite _exitSprite;
    [SerializeField] private bool _isConfigButton;

    private Button _button;

    private void Awake()
    {
        if(gameObject.TryGetComponent(out Button button))
        {
            _button = button;
            _button.onClick.AddListener(Select);
        }
    }

    private void OnEnable()
    {
        if (_exitSprite != null)
        {
            _image.sprite = _exitSprite;
        }
        else
        {
            _image.color = new Color(0, 0, 0, 0);
        }
    }

    private void Select()
    {
        if (_exitSprite != null)
        {
            _image.sprite = _exitSprite;
        }
        else
        {
            _image.color = new Color(0, 0, 0, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isConfigButton)
        {

            _image.color = new Color(1, 1, 1, 0.5f);
            _image.sprite = _hoverSprite;
        }
        else
        {
            _image.color = Color.white;
            _image.sprite = _hoverSprite;

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_exitSprite != null)
        {
            _image.sprite = _exitSprite;
        }
        else
        {
            _image.color = new Color(0, 0, 0, 0);
        }
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (_exitSprite != null)
    //    {
    //        _image.sprite = _exitSprite;
    //    }
    //    else
    //    {
    //        _image.color = new Color(0, 0, 0, 0);
    //    }
    //}
}
