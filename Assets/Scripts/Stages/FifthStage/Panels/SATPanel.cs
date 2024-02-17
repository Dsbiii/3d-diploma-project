using Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector;
using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class SATPanel : MonoBehaviour
    {
        [SerializeField] private DeviceDataPanel _deviceDataPanel;
        [SerializeField] private GameObject _saveButton;
        [SerializeField] private LaptopCablePoint _laptopCablePoint;
        [SerializeField] private CounterCablePoint _counterCablePoint;
        [SerializeField] private FifthStageModel _fifthStageModel;
        [SerializeField] private ButtonsGroup[] _buttonsGroup;
        [SerializeField] private TMP_Dropdown _port;
        [SerializeField] private TMP_Dropdown _mode1;
        [SerializeField] private TMP_Dropdown _mode2;
        [SerializeField] private Button[] _portButtons;
        [SerializeField] private TMP_InputField _passwordField;

        private bool _isSelectedPort;

        private int _mode1Value;
        private int _mode2Value;
        private int _portValue;
        private string _passwordFieldValue;
        private bool _isSave;
        private int _selectedButton;

        public string Port {get;private set;}

        public bool IsPortRight
        {
            get
            {
                if (_port.options[_port.value].text == "СОМ1")
                {
                    return true;
                }
                return false;
            }
        }

        private void Start()
        {
            foreach(var button in _portButtons)
            {
                button.onClick.AddListener(() => { _isSelectedPort = true; Change(); });
            }
            foreach(var button in _buttonsGroup)
            {
                button.OnClick += () => { Change(); };
            } 
            _port.onValueChanged.AddListener((set) => { Change(); });
            _mode1.onValueChanged.AddListener((set) => { Change(); });
            _mode2.onValueChanged.AddListener((set) => { Change(); });
            _passwordField.onValueChanged.AddListener((set) => { Change(); });
        }

        public void Change()
        {
            if (_fifthStageModel.IsRightConnectedComputer && _laptopCablePoint.IsIndicated &&
                _counterCablePoint.IsIndicated)
            {
                _saveButton.SetActive(true);
            }
        }

        public void CheckRight()
        {
            if (_isSave)
            {
                _mode1.value = _mode1Value;
                _mode2.value = _mode2Value;
                _port.value = _portValue;
                _passwordField.text = _passwordFieldValue;
                foreach (var item in _buttonsGroup)
                {
                    item.UnselectColor();
                }
                _buttonsGroup[_selectedButton].Select();
            }
            else
            {
                if (!IsPortRight)
                {
                    _mode1.value = 1;
                    _mode2.value = 1;
                    _port.value = 5;
                    _passwordField.text = "";

                    foreach (var item in _buttonsGroup)
                    {
                        item.UnselectColor();
                    }
                }
            }
        }

        public void Save()
        {
            _saveButton.gameObject.SetActive(false);
            _isSave = true;
            _mode1Value = _mode1.value;
            _mode2Value = _mode2.value;
            _portValue = _port.value;
            Port = _port.options[_port.value].text;
            _passwordFieldValue = _passwordField.text;
            int id = 0;
            foreach(var item in _buttonsGroup)
            {
                if (item.Selected)
                {
                    _selectedButton = id;
                }
                id++;
            }
            foreach(var item in _deviceDataPanel.Devices)
            {
                item.SetPortName(Port);
            }
            _deviceDataPanel.EditPort(Port);
        }

        public bool CheckRightSelect()
        {
            if (_mode1.options[_mode1.value].text == "Нечет" &&
                _mode2.options[_mode2.value].text == "Нечет")
            {
                return true;
            }
            return false;
        }

        public bool IsRight()
        {
            if (_mode1.options[_mode1.value].text == "Нечет" &&
                _mode2.options[_mode2.value].text == "Нечет" &&
                _isSelectedPort &&
                _passwordField.text == "000000")
            {
                return true;
            }
            return false;
        }
    }
}