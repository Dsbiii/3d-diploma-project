using Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector;
using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
using Assets.Scripts.Stages.SecondStage;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class SMPanel : MonoBehaviour
    {
        [SerializeField] private GameObject[] _offPanels;
        [SerializeField] private PortListPanel _portListPanel;
        [SerializeField] private GameObject _writerPanel;
        [SerializeField] private GameObject _readerPanel;
        [SerializeField] private DeviceDataPanel _deviceDataPanel;
        [SerializeField] private PortSettingsPanel _portSettingsPanel;
        [SerializeField] private FifthStageModel _fifthStageModel;
        [SerializeField] private CounterCablePoint _counterCablePoint;
        [SerializeField] private LaptopCablePoint _laptopCablePoint;

        public bool IsReaded {  get; private set; }
        public bool IsWrited { get; private set; }
        public bool IsOpened { get; private set; }

        public void StartWrite()
        {
            if (_counterCablePoint.IsIndicated && _laptopCablePoint.IsIndicated && _fifthStageModel.IsRightConnectedComputer)
            {
                _writerPanel.SetActive(true);
                StartCoroutine(InvokeAction(CloseWritePanel, 5));
            }
        }

        public void StartUpdate()
        {
            if (_counterCablePoint.IsIndicated && _laptopCablePoint.IsIndicated && _fifthStageModel.IsRightConnectedComputer)
            {
                _readerPanel.SetActive(true);
                StartCoroutine(InvokeAction(CloseReaderPanel, 5));
            }
        }

        private void OnEnable()
        {
            IsOpened = true;
        }

        private IEnumerator InvokeAction(System.Action action , float time)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }

        public void OpenOffPanels()
        {
            foreach (var panel in _offPanels)
            {
                panel.SetActive(true);
            }
        }

        public void CloseOffPanels()
        {
            foreach (var panel in _offPanels)
            {
                panel.SetActive(false);
            }
        }

        private void CloseWritePanel()
        {
            IsWrited = true;
            _deviceDataPanel.Write();
            _portSettingsPanel.Write();
            _writerPanel.SetActive(false);  
        }

        private void CloseReaderPanel()
        {
            _portListPanel.Write();
            _deviceDataPanel.UpdateDevices();
            IsReaded = true;
            _readerPanel.SetActive(false);
        }
    }
}