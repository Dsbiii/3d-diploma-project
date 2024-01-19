using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class SelectButtonText : MonoBehaviour
    {
        [SerializeField] private Color _selectColor;
        [SerializeField] private TMP_Text _text;

        private void OnEnable()
        {
            _text.color = Color.black;
        }

        public void Select()
        {
            _text.color = _selectColor;
        }

    }
}