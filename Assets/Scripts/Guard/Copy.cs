using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Copy : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;


    public void CopyBuffer()
    {
        GUIUtility.systemCopyBuffer = _text.text;
    }

}
