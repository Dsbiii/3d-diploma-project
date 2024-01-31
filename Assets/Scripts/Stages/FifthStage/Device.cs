using Assets.Scripts.Stages.FifthStage.Panels;
using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FifthStage
{
    public class Device : MonoBehaviour , IPointerClickHandler
    {
        [SerializeField] private bool _baseDevice;
        [SerializeField] private bool _isInitedToData;
        [SerializeField] private SATPanel _sATPanel;
        [SerializeField] private Color _color;
        [SerializeField] private Image _backGroundPanel;
        [SerializeField] private DeviceDataPanel _deviceDataPanel;
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private TMP_InputField _netAdress;
        [SerializeField] private TMP_InputField _serialNumber;
        [SerializeField] private TMP_InputField _ktt;
        [SerializeField] private TMP_InputField _ktn;
        [SerializeField] private TMP_InputField _port;
        [SerializeField] private TMP_InputField _mode;
        [SerializeField] private TMP_Text _statusText;

        private PortPanelsButton _portPanelsButton;

        public string NetAdress => _netAdress.text;
        public string SerialNumber => _serialNumber.text;
        public string KTT => _ktt.text;
        public string KTN => _ktn.text;
        public string Port => _port.text;
        public string Mode => _mode.text;

        public TMP_InputField NetAdressInputField => _netAdress;
        public TMP_InputField SerialNumberInputField => _serialNumber;
        public TMP_InputField KTTInputField => _ktt;
        public TMP_InputField KTNInputField => _ktn;
        public TMP_InputField PortInputField => _port;
        public TMP_InputField ModeInputField => _mode;
        public TMP_Dropdown Dropdown => _dropdown;

        public int DropDownValue;
        public string NetAdressValue;
        public string SerialNumberValue;
        public string KTTValue;
        public string KTNValue;
        public string PortValue;
        public string ModeValue;

        public bool BaseDevice => _baseDevice;

        private void Awake()
        {
            _deviceDataPanel = FindObjectOfType<DeviceDataPanel>();
            _sATPanel = FindObjectOfType<SATPanel>();
            _portPanelsButton = FindObjectOfType<PortPanelsButton>();

            _netAdress.onValueChanged.AddListener((value) =>
            {
                NetAdressValue = value;
            });
            _serialNumber.onValueChanged.AddListener((value) =>
            {
                SerialNumberValue = value;
            });
            _ktt.onValueChanged.AddListener((value) =>
            {
                KTTValue = value;
            });
            _ktn.onValueChanged.AddListener((value) =>
            {
                KTNValue = value;
            });
            _port.onValueChanged.AddListener((value) =>
            {
                PortValue = value;
            });
            _mode.onValueChanged.AddListener((value) =>
            {
                ModeValue = value;
            });
        }

        private void Start()
        {
            DropDownValue = _dropdown.value;
            NetAdressValue = _netAdress.text;
            KTTValue = _ktt.text;
            PortValue = _port.text;
            KTNValue = _ktn.text;
            SerialNumberValue = _serialNumber.text;

            if(_isInitedToData)
                _deviceDataPanel.AddDevices(this);

            _serialNumber.onSubmit.AddListener(Sumbit);
        }

        public void OnEnable()
        {
            _dropdown.value = DropDownValue;
            _netAdress.text = NetAdressValue;
            _ktt.text = KTTValue;
            _port.text = PortValue;
            _ktn.text = KTNValue;
            _serialNumber.text = SerialNumberValue;
        }

        public void Write()
        {

        }

        public void UpdateDevice()
        {
            _dropdown.value = DropDownValue;
            _netAdress.text = NetAdressValue;
            _ktt.text = KTTValue;
            _port.text = PortValue;
            _ktn.text = KTNValue;
            _serialNumber.text = SerialNumberValue;
            if (_deviceDataPanel.CurrentDevice == this)
            {
                _deviceDataPanel.Ktn.text = KTNValue;
                _deviceDataPanel.Ktt.text = KTTValue;
                _deviceDataPanel.SerialNumber.text = SerialNumberValue;
                _deviceDataPanel.NetAdress.text = NetAdressValue;
            }
        }

        public void Sumbit(string text)
        {
            if (int.TryParse(text, out int result))
            {
                if (result == 55629 || result == 0112055629)
                {
                    _deviceDataPanel.Sumbit(text, this);
                }
            }
        }

        public void SetDeviceValue(string netAdress, string ktt, string ktn , string port , string serialNumber)
        {
            _dropdown.value = 0;
            _netAdress.text = netAdress;
            _ktt.text = ktt;
            _port.text = port;
            _ktn.text = ktn;
            _serialNumber.text = serialNumber;

            DropDownValue = _dropdown.value;
            NetAdressValue = _netAdress.text;
            KTTValue = _ktt.text;
            PortValue = _port.text;
            KTNValue = _ktn.text;
            SerialNumberValue = _serialNumber.text;
        }


        public void Select()
        {
            _backGroundPanel.color = _color;
        }
        
        public void Unselect()
        {
            _backGroundPanel.color = Color.white;
        }

        public void SetStatusText()
        {
            //if (_sATPanel.IsPortRight)
            //    _statusText.text = "ОК";
            _statusText.text = "ОК";

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
            _deviceDataPanel.SetDevicePanel(this);
            //if (_baseDevice)
            //    return;
            _portPanelsButton.OpenDevices();
        }
    }
}