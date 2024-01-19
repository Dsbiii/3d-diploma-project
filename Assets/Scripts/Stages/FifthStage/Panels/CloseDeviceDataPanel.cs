using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class CloseDeviceDataPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _loadPanel;
        [SerializeField] private PortPanelsButton[] _portPanelsButton;
        [SerializeField] private LaptopCablePoint _laptopCablePoint;
        [SerializeField] private DeviceDataPanel _deviceDataPanel;
        [SerializeField] private GameObject[] _panels;

        private void OnEnable()
        {
            if (_laptopCablePoint.IsIndicated)
            {
                //_loadPanel.SetActive(false);
            }
            else
            {
                foreach (var item in _portPanelsButton)
                    item.Close();
                //_loadPanel.SetActive(true);
            }

            foreach (var panel in _panels)
            {
                panel.SetActive(false);
            }
        }

        private void OnDisable()
        {
            _deviceDataPanel.Close();
        }
    }
}