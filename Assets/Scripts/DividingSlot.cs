using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DividingSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _score;

    public void SetText(string text, int score)
    {
        _text.text = text;
        _score.text = score.ToString();
    }
}
