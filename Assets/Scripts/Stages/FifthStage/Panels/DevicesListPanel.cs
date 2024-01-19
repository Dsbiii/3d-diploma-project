using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class DevicesListPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _readDataPanel;
        [SerializeField] private LaptopCablePoint _laptopCablePoint;
        [SerializeField] private GameObject _portList;
        [SerializeField] private GameObject[] _otherPanels;
        [SerializeField] private FifthStageModel _fifthStageModel;


        public void Open()
        {
            if (_laptopCablePoint.IsIndicated && _fifthStageModel.IsRightConnectedComputer)
            {
                _readDataPanel.SetActive(false);
            }
            else
            {
                _readDataPanel.SetActive(true);
                return;
            }
            _portList.SetActive(true);
            foreach (var panel in _otherPanels)
            {
                panel.SetActive(false);
            }
        }

        public void Close()
        {
            _portList.SetActive(false);
        }
    }
}