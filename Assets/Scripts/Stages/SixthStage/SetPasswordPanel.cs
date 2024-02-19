using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class SetPasswordPanel : MonoBehaviour
    {
        [SerializeField] private Toggle _isHidePassword;
        [SerializeField] private TMP_InputField[] _password;
        [SerializeField] private TMP_Text _value;
        [SerializeField] private UsersAndRolesPanel _usersAndRolesPanel;
        [SerializeField] private Button _okkButton;

        private void Awake()
        {
            _isHidePassword.onValueChanged.AddListener(SetupHidePasswords);
        }

        public void Open()
        {
            if (_usersAndRolesPanel.GetUser(out string value))
            {
                _value.text = $"Внимание!\r\nВы собираетесь установить пароль для абонента “{value}”!";
                gameObject.SetActive(true);
            }
        }
        public void CheckPassword()
        {
            if (_password[0].text == _password[1].text && _password[0].text != "" && _password[1].text != "")
            {
                _okkButton.interactable = true;
            }
            else
            {
                _okkButton.interactable = false;
            }
        }
        private void SetupHidePasswords(bool value)
        {
            if (!value)
            {
                foreach(var item in _password)
                {
                    item.contentType = TMP_InputField.ContentType.Password;
                    item.ForceLabelUpdate();
                }
            }
            else
            {
                foreach (var item in _password)
                {
                    item.contentType = TMP_InputField.ContentType.Standard;
                    item.ForceLabelUpdate();
                }
            }
        }
        private void OnEnable()
        {
            _okkButton.interactable = false;
            CheckPassword();
        }
        public void Clean()
        {
            foreach(var item in _password)
            {
                item.text = "";
            }
            _isHidePassword.isOn = false;
        }
    }
}