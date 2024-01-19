using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePanel : MonoBehaviour
{
    [SerializeField] private RectTransform _target;
    [SerializeField] private RectTransform[] _objects;

    private void Update()
    {
        foreach (var obj in _objects)
        {
            obj.sizeDelta = new Vector2(obj.sizeDelta.x, _target.sizeDelta.y);
        }
    }
}
