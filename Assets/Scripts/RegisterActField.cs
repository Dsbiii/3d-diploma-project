using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RegisterActField : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] private UserData _act;
    [SerializeField] private Text _text;
    [SerializeField] private Type _type;
    public bool IsFilled { get; private set; }

    public Text Text => _text;

    public enum Type
    {
        DZO_Name,
        FIO,
        Date_Year,
        Date,
        Date_Time,
        Name,
        SurName,
        Day,
        Mounth,
        Year

    }

    private string[] _mounths = new string[]
    {
        "Январь",
        "Февраль",
        "Март",
        "Апрель",
        "Май",
        "Июнь",
        "Июль",
        "Август",
        "Сентябрь",
        "Ноябрь",
        "Октябрь",
        "Декабрь",

    };

    private void OnEnable()
    {
        _act = FindObjectOfType<UserData>();
        if (gameObject.TryGetComponent(out InputField inputField))
            inputField.enabled = false;
    }

    public void Fill()
    {
        _act = FindObjectOfType<UserData>();

        IsFilled = true;
        transform.GetChild(0).transform.gameObject.SetActive(false);
        if (_type == Type.DZO_Name)
        {
            _text.text = _act.DzoOrgName + " " + _act.DzoPlaceName;
        }
        else if (_type == Type.FIO)
        {
            _text.text = _act.Name + " " + _act.Status;
        }
        else if (_type == Type.Name)
        {
            _text.text = _act.Name;
        }
        else if (_type == Type.SurName)
        {
            _text.text = _act.DzoOrgName;
        }
        else if (_type == Type.Day)
        {
            Debug.Log(_act.Date);
            if (DateTime.TryParse(_act.Date, out DateTime dateTime))
            {
                Debug.Log("Is work");
                _text.text = dateTime.Day.ToString();
            }
        }
        else if (_type == Type.Mounth)
        {
            Debug.Log(_act.Date);

            if (DateTime.TryParse(_act.Date, out DateTime dateTime))
            {
                Debug.Log("Is work");
                _text.text = _mounths[dateTime.Month - 1];
            }
        }
        else if (_type == Type.Year)
        {
            Debug.Log(_act.Date);

            if (DateTime.TryParse(_act.Date, out DateTime dateTime))
            {
                Debug.Log("Is work");
                char[] dates = dateTime.Year.ToString().ToCharArray();
                _text.text = dates[2].ToString() + dates[3].ToString();
            }
        }
        else if (_type == Type.Date)
        {
            Debug.Log(_act.Date);

            if (DateTime.TryParse(_act.Date, out DateTime dateTime))
            {
                Debug.Log("Is work");
                _text.text = dateTime.ToShortDateString();
            }
        }
        else if (_type == Type.Date_Time)
        {
            Debug.Log(_act.Date);

            if (DateTime.TryParse(_act.Date, out DateTime dateTime))
            {
                Debug.Log("Is work");
                _text.text = dateTime.ToString("HH:mm");
            }
        }
        else if (_type == Type.Date_Year)
        {
            Debug.Log(_act.Date);

            if (DateTime.TryParse(_act.Date, out DateTime dateTime))
            {
                Debug.Log("Is work");
                _text.text = dateTime.Year.ToString();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Fill();
    }
}
