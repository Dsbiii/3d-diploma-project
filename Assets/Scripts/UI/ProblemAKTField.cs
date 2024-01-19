using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProblemAKTField : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private BreakingInstumentSevice _breakingInstumentSevice;

    private void Start()
    {
        _text.text = _breakingInstumentSevice.Problem;
    }
}