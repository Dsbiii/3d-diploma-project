using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Text _toggleValue;

    public bool IsOn => _toggle.isOn;
    public string Value => _toggleValue.text;
    public bool IsError;
    public string Error { get; set; }
    public bool IsDismantrilg;

    //private void OnEnable()
    //{
    //    _toggle = GetComponentInChildren<Toggle>();
    //    _toggleValue = GetComponentInChildren<Text>();
    //}

}
