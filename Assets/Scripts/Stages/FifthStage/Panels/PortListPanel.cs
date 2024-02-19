using Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector;
using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class PortListPanel : MonoBehaviour
    {
        [SerializeField] private DeviceDataPanel _deviceDataPanel;
        [SerializeField] private GameObject[] _dataObjects;
        [SerializeField] private FifthStageModel _fifthStageModel;
        [SerializeField] private GameObject _readDataPanel;
        [SerializeField] private CounterCablePoint _counterCablePoint;
        [SerializeField] private LaptopCablePoint _laptopCablePoint;
        [SerializeField] private TMP_InputField _namePort;
        [SerializeField] private TMP_Dropdown _portDropdown;
        [SerializeField] private GameObject _portList;
        [SerializeField] private GameObject[] _otherPanels;

        [SerializeField] private List<Port> _ports = new List<Port>();

        private List<Port> _tempPorts = new List<Port>();
        private Port _selectedPort;
       
        public string PortName { get; private set; }

        public bool IsRightCreatedPort { get; private set; }

        public IEnumerable<Port> Ports => _ports;

        public void AddPort(Port port)
        {
            //_ports.Add(port);
            _tempPorts.Add(port);
        }

        public void DeleteSelected(Port port)
        {
            if (port != null)
            {
                _ports.Remove(port);
                _tempPorts.Remove(port);
                Destroy(port.gameObject);
                if(port == _selectedPort)
                {
                    _selectedPort = null;
                }
            }
        }

        public void SelectPort(Port port)
        {       
            if (_selectedPort != null && _selectedPort != port)
            {
                _selectedPort.UnSelect();
            }
            _selectedPort = port;
            if(_selectedPort.IsBase)
            {
                foreach(var item in _dataObjects)
                {
                    item.SetActive(false);
                }
            }
            else
            {
                foreach (var item in _dataObjects)
                {
                    item.SetActive(true);
                }
            }
        }

        public void Write()
        {
            if(_tempPorts.Count > 0)
            {
                IsRightCreatedPort = true;
            }
            _ports.AddRange(_tempPorts);
            _tempPorts.Clear();
            if (_selectedPort != null)
            {
                _selectedPort.SetValue(_namePort.text);
                if(_portList.activeSelf)
                    _deviceDataPanel.EditPort(_namePort.text);
                PortName = _namePort.text;
                //_namePort.text = _selectedPort.NamePortText;
            }
        }


        public void Open()
        {
            if ((_counterCablePoint.IsIndicated && _laptopCablePoint.IsIndicated && _fifthStageModel.IsRightConnectedComputer) ||
                (_laptopCablePoint.IsIndicated && _fifthStageModel.IsRightConnectedComputer))
            {
                _readDataPanel.SetActive(false);
            }
            else
            {
                _readDataPanel.SetActive(true);
                return;
            }
            foreach (var item in _dataObjects)
            {
                item.SetActive(false);
            }
            _portList.SetActive(true);
            foreach(var panel in _otherPanels)
            {
                panel.SetActive(false);
            }
        }

        public void Close()
        {
            for(int i = 0; i < _tempPorts.Count; i++)
            {
                Destroy(_tempPorts[i].gameObject);
            }
            _tempPorts.Clear();
            _portList.SetActive(false);
        }
    }
}