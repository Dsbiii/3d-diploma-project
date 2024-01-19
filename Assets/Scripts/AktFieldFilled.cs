using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AktFieldFilled : MonoBehaviour , IPointerClickHandler
{
    private enum InstrumentType { CE602M , Parma , Clamps , Timer }

    public bool IsGetText = true;
    public bool OffPlaceholder = true;
    [SerializeField] private Act _act;
    [SerializeField] private InstrumentType _instrumentType;
    [SerializeField] private bool _isValueFromAct;
    
    [SerializeField] private string _value;
    [SerializeField] private Text _text;

    public Text Text => _text;


    public bool IsFilled
    {
        get
        {
            try
            {
                return _text.text.Length > 0;
            }
            catch
            {
                return true;
            }
        }
    }

    private void OnEnable()
    {

        if (IsGetText)
        {
            if(transform.childCount == 2)
            {
                _text = transform.GetChild(1).transform.GetComponent<Text>();
            }else if(transform.childCount == 1)
            {
                _text = transform.GetChild(0).transform.GetComponent<Text>();
            }
        }
        if (_text == null)
            Debug.LogError("_text in null");
        if (gameObject.TryGetComponent(out InputField inputField))
            inputField.enabled = false;
    }

    public void Fill()
    {
        if (OffPlaceholder)
            transform.GetChild(0).transform.gameObject.SetActive(false);
        if (_text == null)
            return;
        if (_isValueFromAct)
        {
            if (_instrumentType == InstrumentType.CE602M)
            {
                _text.text = _act.Instrumental._CE602MDate;
            }
            else if (_instrumentType == InstrumentType.Parma)
            {
                _text.text = _act.Instrumental._ParmaDate;
            }
            else if (_instrumentType == InstrumentType.Clamps)
            {
                _text.text = _act.Instrumental._ClampsDate;
            }
            else if (_instrumentType == InstrumentType.Timer)
            {
                _text.text = _act.Instrumental._TimerDate;
            }
        }
        else
        {
            _text.text = _value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Fill();
    }
}
