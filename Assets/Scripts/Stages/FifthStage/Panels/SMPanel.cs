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

        public bool IsReaded {  get; private set; }
        public bool IsWrited { get; private set; }

        public void StartWrite()
        {
            _writerPanel.SetActive(true);
            Invoke(nameof(CloseWritePanel),5);
        }

        public void StartUpdate()
        {
            _readerPanel.SetActive(true);
            Invoke(nameof(CloseReaderPanel), 5);
        }

        public void OpenOffPanels()
        {
            //foreach(var panel in _offPanels)
            //{
            //    panel.SetActive(true);
            //}
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