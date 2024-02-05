using Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector;
using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class DeviceDataPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _readDataPanel;
        [SerializeField] private LaptopCablePoint _laptopCablePoint;
        [SerializeField] private CounterCablePoint _counterCablePoint;
        [SerializeField] private Device _firstDevice;
        [SerializeField] private PortListPanel _portListPanel;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private TMP_Dropdown _portDropDown;
        [SerializeField] private TMP_InputField _netAdress;
        [SerializeField] private TMP_InputField _serialNumber;
        [SerializeField] private TMP_InputField _ktt;
        [SerializeField] private TMP_InputField _ktn;
        [SerializeField] private TMP_Text _port;
        [SerializeField] private TMP_Text _mode;
        [SerializeField] private TMP_Text _date;
        [SerializeField] private TogglesPanel _togglesPanel;
        [Inject] private FifthStageExam _fifthStageExam;

        public Device CurrentDevice { get; private set; }
        
        [SerializeField] private List<Device> _devices = new List<Device>();

        private List<Device> _tempDevices = new List<Device>();

        public TMP_InputField NetAdress => _netAdress;
        public TMP_InputField SerialNumber => _serialNumber;
        public TMP_InputField Ktt => _ktt;
        public TMP_InputField Ktn => _ktn;
        public TMP_Text Port => _port;
        public TMP_Text Mode => _mode;
        public TMP_Text Date => _date;
        private void Start()
        {
            _serialNumber.onSubmit.AddListener(Sumbit);
            _serialNumber.onValueChanged.AddListener((value) => {
                if(CurrentDevice != null)
                {
                    CurrentDevice.SerialNumberValue = value;
                }
            });
            _netAdress.onValueChanged.AddListener((value) => {
                if (CurrentDevice != null)
                {
                    CurrentDevice.NetAdressValue = value;
                }
            });
            _ktt.onValueChanged.AddListener((value) => {
                if (CurrentDevice != null)
                {
                    CurrentDevice.KTTValue = value;
                }
            });
            _ktn.onValueChanged.AddListener((value) => {
                if (CurrentDevice != null)
                {
                    CurrentDevice.KTNValue= value;
                }
            });
        }

        public void DeleteSelected(Device device)
        {
            if (device != null)
            {
                _devices.Remove(device);
                _tempDevices.Remove(device);
                Destroy(device.gameObject);

                if (CurrentDevice == device)
                {
                    CurrentDevice = null;
                }
            }
        }

        private void Update()
        {
            DateTime currentDateTime = DateTime.Now;

            string formattedDateTime = currentDateTime.ToString("HH:mm dd.MM.yyyy");

            _date.text = formattedDateTime;
        }

        public void Write()
        {
            _devices.AddRange(_tempDevices);
            _tempDevices.Clear();

            foreach (var item in _devices)
            {
                item.Write();
            }

            //if(CurrentDevice != null )
            //{
            //    CurrentDevice.SetDeviceValue(_netAdress.text, _ktt.text, _ktn.text,
            //                    _port.text, "55629");
            //}
        }

        public void Sumbit(string text)
        {
            if (CurrentDevice != null)
            {
                if (int.TryParse(text, out int result))
                {
                    if (result == 55629 || result == 0112055629)
                    {
                        _serialNumber.text = "0112055629";
                        _netAdress.text = "29";
                        _ktn.text = "1";
                        _ktt.text = "1";
                        _port.text = "Последовательный порт";
                        CurrentDevice.KTTValue = "1";
                        CurrentDevice.KTNValue = "1";
                        CurrentDevice.NetAdressValue = "29";
                        CurrentDevice.PortValue = "Последовательный порт";
                        CurrentDevice.SerialNumberValue = "0112055629";
                    }
                }
            }
        }

        public void Sumbit(string text , Device device)
        {
            if (int.TryParse(text, out int result))
            {
                if (result == 55629 || result == 0112055629)
                {
                    _serialNumber.text = text;
                    device.SetDeviceValue("29", "1", "1",
                          "Последовательный порт", _serialNumber.text);
                }
            }
        }

        public void UpdateDevices()
        {
            foreach(var item in _devices)
            {
                if(item.SerialNumber == "0112055629" ||
                    item.SerialNumber == "55629")
                {
                    string port = _portDropDown.options[_portDropDown.value].text;
                    item.SetDeviceValue("29", "1", "1",
                        port, "0112055629");
                }
            }

            foreach (var device in _devices)
            {
                if (device.Dropdown.options[device.Dropdown.value].text == "СЭТ-4ТМ.03М")
                {
                    string port = _portDropDown.options[_portDropDown.value].text;

                    device.SetDeviceValue("29", "1", "1",
                        port, "0112055629");
                    //_portDropDown.value = 2;
                }
                device.UpdateDevice();
            }
        }

        public void AddDevices(Device device)
        {
            _fifthStageExam.ConfiguredDevice = true;
            //_devices.Add(device);
            _tempDevices.Add(device);
        }


        public void SetDevicePanel(Device device)
        {
            if (CurrentDevice != null && CurrentDevice != device)
            {
                CurrentDevice.Unselect();
            }
            CurrentDevice = device;
            _ktn.text = CurrentDevice.KTTValue;
            _ktt.text = CurrentDevice.KTTValue;
            _netAdress.text = CurrentDevice.NetAdressValue;
            _serialNumber.text = CurrentDevice.SerialNumberValue;
        }

        public void SetDevicesStatus()
        {
            if (!_portListPanel.IsRightCreatedPort)
                return;

            if (!_counterCablePoint.IsIndicated)
                return;

            foreach (var item in _devices)
            {
                item.SetStatusText();
            }
        }

        public void Open()
        {
            if (_laptopCablePoint.IsIndicated)
            {
                _readDataPanel.SetActive(false);
            }
            else
            {
                _readDataPanel.SetActive(true);
                return;
            }
            _panel.SetActive(true);
        }

        public void Close()
        {
            for (int i = 0; i < _tempDevices.Count; i++)
            {
                Destroy(_tempDevices[i].gameObject);
            }
            _password.text = "";
            _portDropDown.value = 0;
            _tempDevices.Clear();
            _panel.SetActive(false);
        }
    }
}