using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Size : MonoBehaviour
{
    public RectTransform[] _Rects;

    //private void Awake()
    //{
    //    _Rect.sizeDelta = new Vector2(_Rect.sizeDelta.x, _rectText.preferredHeight);
    //    _max = GetComponent<RectTransform>().sizeDelta.y;
    //    if(_Rect.sizeDelta.y > _max)
    //    {
    //        _max = _Rect.sizeDelta.y;
    //    }
    //    if (_Rect2.sizeDelta.y > _max)
    //    {
    //        _max = _Rect2.sizeDelta.y;
    //    }
    //    GetComponent<RectTransform>().sizeDelta = new Vector2(1561.507f, GetComponent<RectTransform>().sizeDelta.y);
    //}

    private void Update()
    {
        float max = _Rects.Select(item => item.rect.height).Max();
        GetComponent<RectTransform>().sizeDelta = new Vector2(1561.507f, max);
        foreach (var rect in _Rects)
            rect.GetComponent<RectTransform>().sizeDelta = new Vector2(rect.rect.width, max);
    }
}
