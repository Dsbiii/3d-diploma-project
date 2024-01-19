using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class PortSettingsPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _countErrorsInputField;

        private string _countErrors;

        public void Write()
        {
            _countErrors = _countErrorsInputField.text;
        }

        private void OnEnable()
        {
            _countErrorsInputField.text = _countErrors;
        }

    }
}