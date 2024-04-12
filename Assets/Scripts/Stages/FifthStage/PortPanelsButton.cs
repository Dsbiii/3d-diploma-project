using Assets.Scripts.Stages.FifthStage.Panels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FifthStage
{
    public class PortPanelsButton : MonoBehaviour
    {
        [SerializeField] private DeviceDataPanel _deviceDataPanel;
        [SerializeField] private GameObject _dataInfoPanel;
        [SerializeField] private GameObject[] _devicePanel;

        [SerializeField] private Color _disableColor;
        [SerializeField] private Color _enableColor;

        [SerializeField] private Image _devicesButton;
        [SerializeField] private Image _dataButton;

        [SerializeField] private GameObject _devicesPanel;
        [SerializeField] private GameObject _dataPanel;

        public void OpenDevices()
        {
            if(_deviceDataPanel.CurrentDevice == null || _deviceDataPanel.CurrentDevice.BaseDevice)
            {
                foreach (var device in _devicePanel)
                {
                    device.SetActive(false);
                }
            }
            else
            {
                foreach (var device in _devicePanel)
                {
                    device.SetActive(true);
                }
            }
            _devicesButton.color = _enableColor;
            _dataButton.color = _disableColor;
            _devicesPanel.SetActive(true);
            _dataPanel.SetActive(false);
        }

        public void Close()
        {
            foreach (var device in _devicePanel)
            {
                device.SetActive(false);
            }
            _dataInfoPanel.SetActive(false);
        }

        public void OpenData()
        {
            if (_deviceDataPanel.CurrentDevice != null && _deviceDataPanel.CurrentDevice.BaseDevice)
            {
                _dataInfoPanel.SetActive(false);
            }
            else
            {
                _dataInfoPanel.SetActive(true);
            }
            _devicesButton.color = _disableColor;
            _dataButton.color = _enableColor;
            _dataPanel.SetActive(true);
            _devicesPanel.SetActive(false);
        }
    }
}