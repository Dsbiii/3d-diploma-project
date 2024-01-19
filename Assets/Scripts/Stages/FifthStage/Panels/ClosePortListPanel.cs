using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class ClosePortListPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _loadPanel;
        [SerializeField] private PortPanelsButton[] _portPanelsButton;
        [SerializeField] private LaptopCablePoint _laptopCablePoint;
        [SerializeField] private PortListPanel _portListPanel;

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
        }

        private void OnDisable()
        {
            _portListPanel.Close();
        }

    }
}