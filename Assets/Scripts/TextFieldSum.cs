using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextFieldSum : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool _removeLastValue;
    [SerializeField] private bool _isRound;
    [SerializeField] private Text[] _textsToSum;
    private Dropdown _dropdown;
    private string _rightValue;

    public Dropdown Dropdown => _dropdown;
    public float Value
    {
        get
        {
            try
            {
                return float.Parse(_dropdown.options[_dropdown.value].text);
            }
            catch
            {
                return 0;
            }
        }
    }
    public bool IsRight
    {
        get
        {
            try
            {
                return _dropdown.options[_dropdown.value].text.Length > 0;
            }
            catch
            {
                return true;
            }
        }
    }

    //public bool IsRight => _rightValue == _dropdown.options[_dropdown.value].text;

    private void OnEnable()
    {
        Invoke(nameof(DisplaySum), 0.1f);
    }

    private void DisplaySum()
    {
        _dropdown = GetComponent<Dropdown>();
        string sum = GetSumsOfTexts().ToString();
        //if (_isRound)
        //    sum = Mathf.Round((GetSumsOfTexts() / 1000)).ToString();
        //else
        //    sum = GetSumsOfTexts().ToString();
        _dropdown.options[1].text = sum;
        _rightValue = sum;
        _dropdown.options[2].text = Random.Range(GetSumsOfTexts() - 10, GetSumsOfTexts() + 10).ToString();
        _dropdown.options[3].text = Random.Range(GetSumsOfTexts() - 10, GetSumsOfTexts() + 10).ToString();
        _dropdown.options[4].text = Random.Range(GetSumsOfTexts() - 10, GetSumsOfTexts() + 10).ToString();
        _dropdown.options[5].text = Random.Range(GetSumsOfTexts() - 10, GetSumsOfTexts() + 10).ToString();

        _dropdown.ClearOptions();
        _dropdown.AddOptions(new List<Dropdown.OptionData> { new Dropdown.OptionData(_rightValue) });
        _dropdown.value = 0;
    }

    private float GetSumsOfTexts()
    {
        float value = 0;
        foreach(var text in _textsToSum)
        {
            string textValue = text.text;

            if (_removeLastValue)
            {
                textValue = textValue.Remove(textValue.Length - 1);
            }

            if (textValue.Length > 0 && float.TryParse(textValue, out float result))
            {   
                value += result;
            }
        }
        return value;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        string sum = GetSumsOfTexts().ToString();
        //if (_isRound)
        //    sum = Mathf.Round((GetSumsOfTexts() / 1000)).ToString();
        //else
        //    sum = GetSumsOfTexts().ToString();
        _dropdown.options[1].text = sum;
        _rightValue = sum;
        _dropdown.options[2].text = Random.Range(GetSumsOfTexts() - 10, GetSumsOfTexts() + 10).ToString();
        _dropdown.options[3].text = Random.Range(GetSumsOfTexts() - 10, GetSumsOfTexts() + 10).ToString();
        _dropdown.options[4].text = Random.Range(GetSumsOfTexts() - 10, GetSumsOfTexts() + 10).ToString();
        _dropdown.options[5].text = Random.Range(GetSumsOfTexts() - 10, GetSumsOfTexts() + 10).ToString();

        GetComponent<Dropdown>().ClearOptions();
        GetComponent<Dropdown>().AddOptions(new List<Dropdown.OptionData> { new Dropdown.OptionData(_rightValue) });
        GetComponent<Dropdown>().value = 0;
    }
}
