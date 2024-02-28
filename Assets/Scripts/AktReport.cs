using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using Assets.Scripts.Stages.FourthStage;

public class AktReport : MonoBehaviour
{
    [SerializeField] private string _actName;
    [SerializeField] private GameObject[] _aktPage;
    [SerializeField] private List<SelfFilledIField> _selfFilledIFields;
    [SerializeField] private List<Toggle> _toggles; 
    [SerializeField] private List<SelectDrop> _selectDrops;
    [SerializeField] private List<AktFieldFilled> _aktFieldFilleds;
    [SerializeField] private List<RegisterActField> _registerActField;
    [SerializeField] private List<TextFieldSum> _textFieldSums;
    [SerializeField] private List<InputField> _inputFields;
    [SerializeField] private TMP_Text _textError;
    [Inject] private FourthStageExamSystem _fourthStageExamSystem;
    [Inject] private FourthStageModel _fourthStageModel;
    public string TextError => _textError.text;
    public IReadOnlyList<GameObject> AktPage => _aktPage;
    public IReadOnlyList<SelfFilledIField> SelfFilledIFields => _selfFilledIFields;
    public IReadOnlyList<Toggle> Toggles => _toggles;
    public IReadOnlyList<SelectDrop> SelectDrops => _selectDrops;
    public IReadOnlyList<AktFieldFilled> AktFieldFilleds => _aktFieldFilleds;
    public IReadOnlyList<RegisterActField> RegisterActField => _registerActField;
    public IReadOnlyList<TextFieldSum> TextFieldSums => _textFieldSums;
    public IReadOnlyList<InputField> InputFields => _inputFields;

    public bool IsOpened { get; private set; }
    public bool IsOpenedBeforeExitFromTP { get; private set; }

    private void OnEnable()
    {
        IsOpened = true;
        if(_fourthStageModel.IsExitedFromTP)
        {
            IsOpenedBeforeExitFromTP = true;
        }
        if (!FindObjectOfType<PlakatService>().IsSetupedPlakat)
        {
            _fourthStageExamSystem.SetCriticalError();
        }
        //_selfFilledIFields = GetComponentsInChildren<SelfFilledIField>().ToList();
        //_selectDrops = GetComponentsInChildren<SelectDrop>().ToList();
        //_aktFieldFilleds = GetComponentsInChildren<AktFieldFilled>().ToList();
        //_registerActField = GetComponentsInChildren<RegisterActField>().ToList();
        //_textFieldSums = GetComponentsInChildren<TextFieldSum>().ToList();
    }

    //private void OnEnable()
    //{
    //    //_inputFields = GetComponentsInChildren<InputField>().ToList();
    //    var fields = GetComponentsInChildren<AktFieldFilled>().ToList();

    //    foreach(var item in fields)
    //    {
    //        if (item.enabled)
    //        {
    //            _aktFieldFilleds.Add(item);
    //        }
    //    }
    //}


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(CheckForRightFillAkt());

            //_selectDrops.ForEach(item => Debug.Log(!item.IsRight ? item.ToString() : ""));
            //_aktFieldFilleds.ForEach(item => Debug.Log(!item.IsFilled ? item.ToString() : ""));
            //_registerActField.ForEach(item => Debug.Log(!item.IsFilled ? item.ToString() : ""));
            //_textFieldSums.ForEach(item => Debug.Log(!item.IsRight ? item.ToString() : ""));
            //_toggles.ForEach(item => Debug.Log(!item.isOn ? item.ToString() : ""));
            //_selfFilledIFields.ForEach(item => Debug.Log(!item.IsFilled ? item.ToString() : ""));
            //_inputFields.ForEach(item => Debug.Log(item + " " + (item.text.Length > 0)));
        }
    }

    public void RightField()
    {
        foreach(var item in _registerActField)
        {
            item.Fill();
        }
        foreach(var item in _selectDrops)
        {
            item.Fill();
        }
        foreach (var item in _aktFieldFilleds)
        {
            item.Fill();
        }
        foreach(var item in _selfFilledIFields)
        {
            item.Fill(); 
        }
    }

    public bool CheckForRightFillAkt()
    {
        Debug.Log("_inputFields.Where(item => item.text.Length > 0).ToArray().Length " + _inputFields.Where(item => item.text.Length > 0).ToArray().Length);
        Debug.Log("_inputFields.Count " + _inputFields.Count);
        if(_inputFields.Where(item => item.text.Length > 0).ToArray().Length >= _inputFields.Count - 80)
        {
            return true;
        }
        else
        {
            return false;
        }

        //if (_selectDrops.Where(item => item.IsRight).ToArray().Length == _selectDrops.Count &&
        //    _aktFieldFilleds.Where(item => item.IsFilled).ToArray().Length == _aktFieldFilleds.Count &&
        //    _registerActField.Where(item => item.IsFilled).ToArray().Length == _registerActField.Count &&
        //    _textFieldSums.Where(item => item.IsRight).ToArray().Length == _textFieldSums.Count &&
        //    _toggles.Where(item => item.isOn).ToArray().Length == _toggles.Count &&
        //    _selfFilledIFields.Where(item => item.IsFilled).ToArray().Length == _selfFilledIFields.Count)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
    }

}
