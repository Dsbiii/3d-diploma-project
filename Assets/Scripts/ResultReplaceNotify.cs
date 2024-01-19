using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultReplaceNotify : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void NotifyRightReplace()
    {
        _text.text = "Предмет заменен согласно требованию ТБ";
        _text.gameObject.SetActive(true);
    }

    public void NotiftWrongReplace()
    {
        _text.text = "Замена исправного инструмента не произведена";
        _text.gameObject.SetActive(true);
    }

    public void CloseNotifyText()
    {
        _text.gameObject.SetActive(false);
    }
}
