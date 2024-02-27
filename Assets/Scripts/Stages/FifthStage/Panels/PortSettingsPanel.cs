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
        public bool WritedCountErrors { private set; get; }

        public void Write()
        {
            _countErrors = _countErrorsInputField.text;
            if(gameObject.activeSelf)
                WritedCountErrors = true;
        }

        private void OnEnable()
        {
            _countErrorsInputField.gameObject.SetActive(true);
            _countErrorsInputField.text = _countErrors;

            //if (!_portListPanel.IsRightCreatedPort)
            //{
            //    _countErrorsInputField.gameObject.SetActive(false);
            //}
            //else
            //{
            //    _countErrorsInputField.gameObject.SetActive(true);
            //    _countErrorsInputField.text = _countErrors;
            //}
        }

    }
}