using System.Collections;
using UnityEngine;
using Assets.Scripts.Stages;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M;
using Assets.Scripts.Stages.SecondStage.Electric_Box.Parma;
using Assets.Scripts.Stages.SecondStage.Panels;


namespace Assets.Scripts.Instruments
{
    public class WatchService : MonoBehaviour
    {
        [SerializeField] private GameObject _watch;
        [SerializeField] private ItemPreviewPanel _itemPreviewPanel;
        [SerializeField] private SecondStagePanel _secondStagePanel;
        [SerializeField] private ItemPreview _firstStageItemPreview;

        private bool _isOpen;

        public void Close()
        {
            _secondStagePanel.Open();
            _itemPreviewPanel.CloseInventoryPanel();
            _watch.SetActive(false);
            _isOpen = false;
        }

        public void Swtich(Item item)
        {
            if (_isOpen)
            {
                _secondStagePanel.Open();
                _itemPreviewPanel.CloseInventoryPanel();
                _watch.SetActive(false);
                _isOpen = false;
            }
            else
            {
                _secondStagePanel.Open();
                _firstStageItemPreview.SelectItemInstrument(item);
                _watch.SetActive(true);
                _isOpen = true;
            }
        }

    }
}