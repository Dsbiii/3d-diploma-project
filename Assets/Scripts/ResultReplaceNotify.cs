using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultReplaceNotify : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void NotifyRightReplace()
    {
        _text.text = "������� ������� �������� ���������� ��";
        _text.gameObject.SetActive(true);
    }

    public void NotiftWrongReplace()
    {
        _text.text = "������ ���������� ����������� �� �����������";
        _text.gameObject.SetActive(true);
    }

    public void CloseNotifyText()
    {
        _text.gameObject.SetActive(false);
    }
}
