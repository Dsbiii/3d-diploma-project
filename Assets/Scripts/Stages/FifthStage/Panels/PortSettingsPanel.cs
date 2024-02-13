using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class PortSettingsPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _countErrorsInputField;
        [SerializeField] private PortListPanel _portListPanel;

        private string _countErrors;

        public void Write()
        {
            _countErrors = _countErrorsInputField.text;
        }

        private void OnEnable()
        {
            if(!_portListPanel.IsRightCreatedPort)
            {
                _countErrorsInputField.gameObject.SetActive(false);
            }
            else
            {
                _countErrorsInputField.gameObject.SetActive(true);
                _countErrorsInputField.text = _countErrors;
            }
        }

    }
}