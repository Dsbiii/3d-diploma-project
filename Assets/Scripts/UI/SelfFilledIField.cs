using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SelfFilledIField : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private string _text;

    public bool IsFilled => _inputField.text.Length > 0;

    public InputField InputField => _inputField;

    [SerializeField] private Text _textComponent;

    private void OnEnable()
    {
        _inputField = GetComponent<InputField>();
        _textComponent = transform.GetChild(1).transform.GetComponent<Text>();
    }

    public void Fill()
    {
        _inputField = GetComponent<InputField>();
        _inputField.enabled = false;
        _textComponent = transform.GetChild(1).transform.GetComponent<Text>();
        _textComponent.text = _text;
    }
}
