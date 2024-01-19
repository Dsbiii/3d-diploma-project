using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{
    public RectTransform _Rect;
    public RectTransform _Rect2;

    private float _max;

    private void Awake()
    {
        _max = GetComponent<RectTransform>().sizeDelta.y;
        if(_Rect.sizeDelta.y > _max)
        {
            _max = _Rect.sizeDelta.y;
        }
        if (_Rect2.sizeDelta.y > _max)
        {
            _max = _Rect2.sizeDelta.y;
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(1561.507f, GetComponent<RectTransform>().sizeDelta.y);
    }

    private void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(1561.507f, _max);
    }
}
