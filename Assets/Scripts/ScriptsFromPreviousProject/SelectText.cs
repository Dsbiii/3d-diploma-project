using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectText : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private Text _placeholder;

    private void Awake()
    {
        transform.Find("Text").GetComponent<Text>().enabled = false;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        transform.Find("Text").GetComponent<Text>().enabled = true;
        if (_placeholder != null)
            _placeholder.enabled = false;
    }
}
