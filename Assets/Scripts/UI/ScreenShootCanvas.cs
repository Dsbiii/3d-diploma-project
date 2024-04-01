using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShootCanvas : MonoBehaviour
{
    [SerializeField] private AktReport _aktReportToCopy;
    [SerializeField] private AktReport _aktReport;
    [SerializeField] private GameObject[] _aktPage;
    [SerializeField] private List<SelfFilledIField> _selfFilledIFields;
    [SerializeField] private List<Toggle> _toggles;
    [SerializeField] private List<SelectDrop> _selectDrops;
    [SerializeField] private List<AktFieldFilled> _aktFieldFilleds;
    [SerializeField] private List<RegisterActField> _registerActField;
    [SerializeField] private List<Text> _textFieldSums;
    [SerializeField] private List<InputField> _inputFields;
    [SerializeField] private List<Text> _textSecond;
    [SerializeField] private TMP_Text _textError;

    //private void OnEnable()
    //{
    //    _selectDrops = _aktReportToCopy.SelectDrops.ToList();
    //    _aktFieldFilleds = _aktReportToCopy.AktFieldFilleds.ToList();
    //    _registerActField = _aktReportToCopy.RegisterActField.ToList();
    //    _textFieldSums = _aktReportToCopy.TextFieldSums.ToList();
    //    _selfFilledIFields = _aktReportToCopy.SelfFilledIFields.ToList();
    //    _toggles = _aktReportToCopy.Toggles.ToList();
    //}

    //private void OnEnable()
    //{
    //    _inputFields = GetComponentsInChildren<InputField>().ToList();
    //}


    public bool PrepareForScreenShoot()
    {
        //_aktPage[0].SetActive(_aktReport.AktPage[0].activeSelf);
        //_aktPage[1].SetActive(_aktReport.AktPage[1].activeSelf);

        _textError.text = _aktReport.TextError;

        for (int i = 0; i < _textSecond.Count; i++)
        {
            if (_textSecond[i] != null && _aktReport.TextSecond[i] != null)
            {
                _textSecond[i].text = _aktReport.TextSecond[i].text;
            }
        }


        for (int i = 0; i < _selectDrops.Count; i++)
        {

            if (_selectDrops[i].Dropdown != null && _aktReport.SelectDrops[i].Dropdown != null)
            {
                _selectDrops[i].Dropdown.captionText.text = _aktReport.SelectDrops[i].Dropdown.options[_aktReport.SelectDrops[i].Dropdown.value].text.ToString();
            }
        }
        for (int i = 0; i < _aktFieldFilleds.Count; i++)
        {
            if (_aktFieldFilleds[i].Text != null && _aktReport.AktFieldFilleds[i].Text != null)
            {
                _aktFieldFilleds[i].Text.text = _aktReport.AktFieldFilleds[i].Text.text;
                if (_aktFieldFilleds[i].transform.childCount > 1)
                    _aktFieldFilleds[i].transform.GetChild(0).transform.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < _registerActField.Count; i++)
        {
            if (_registerActField[i].Text != null && _aktReport.RegisterActField[i].Text != null)
            {
                _registerActField[i].Text.text = _aktReport.RegisterActField[i].Text.text;
                if (_registerActField[i].transform.childCount > 1)
                    _registerActField[i].transform.GetChild(0).transform.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < _textFieldSums.Count; i++)
        {
            if (_textFieldSums[i].text != null && _aktReport.TextFieldSums[i].Dropdown != null)
            {
                _textFieldSums[i].text = _aktReport.TextFieldSums[i].Value.ToString();
            }
        }
        for (int i = 0; i < _selfFilledIFields.Count; i++)
        {
            if (_selfFilledIFields[i].InputField != null && _aktReport.SelfFilledIFields[i].InputField != null)
            {
                _selfFilledIFields[i].InputField.text = _aktReport.SelfFilledIFields[i].InputField.text;
                if (_selfFilledIFields[i].transform.childCount > 1)
                    _selfFilledIFields[i].transform.GetChild(0).transform.gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < _toggles.Count; i++)
        {
            if (_toggles[i] != null && _toggles[i] != null)
                _toggles[i].isOn = _aktReport.Toggles[i].isOn;
        }

        for(int i = 0; i < _inputFields.Count; i++)
        {
            _inputFields[i].text = _aktReport.InputFields[i].text;
            if (_inputFields[i].transform.childCount > 1)
                _inputFields[i].transform.GetChild(0).transform.gameObject.SetActive(false);
        }
        Debug.Log("is work");
        return true;
    }




}
