using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FIOActField : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UserData _act;
    [SerializeField] private Text _text;
    private void OnEnable()
    {
        _act = FindObjectOfType<UserData>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _text.text = _act.Status + " " + _act.Name;
    }
}
