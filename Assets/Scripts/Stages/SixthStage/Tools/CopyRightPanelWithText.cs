using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage.Tools
{
    public class CopyRightPanelWithText : MonoBehaviour
    {
        [SerializeField] private string _text;
        [SerializeField] private TMP_Text[] _textFrom;
        [SerializeField] private TMP_Text[] _textTo;

        public void CopyRight()
        {
            for (int i = 0; i < _textFrom.Length; i++)
            {
                _textTo[i].text = _textFrom[i].text + _text;
            }
        }
    }
}