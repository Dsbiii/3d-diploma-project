using Assets.Scripts.Stages;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.SecondStage.Dismantling;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface;
using Assets.Scripts.Stages.SecondStage.Electric_Box.Parma;
using Assets.Scripts.Stages.SecondStage.Panels;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Instruments
{
    public class GerconService
    {
        private GerconInstrument _gerconInstrument;
        private ElectricClampInstruments _electricClampInstruments;
        private ParmaInstrument _parmaInstrument;
        private CE602MInstrument _cE602MInstrument;
        private ItemPreviewPanel _itemPreviewPanel;
        private SecondStagePanel _secondStagePanel;
        private ItemPreview _firstStageItemPreview;
        private CE602MInterface _cE602MInterface;
        private bool _isOpen;
        public bool IsOpen => _isOpen;
        public bool IsWasTaked { get; private set; }

        public GerconService(GerconInstrument gerconInstrument,CE602MInterface cE602MInterface, ItemPreviewPanel itemPreviewPanel, ElectricClampInstruments electricClampInstruments, ParmaInstrument parmaInstrument, CE602MInstrument cE602MInstrument,
                SecondStagePanel secondStagePanel, ItemPreview itemPreview)
        {
            _gerconInstrument = gerconInstrument;
            _cE602MInterface = cE602MInterface;
            _itemPreviewPanel = itemPreviewPanel;
            _electricClampInstruments = electricClampInstruments;
            _parmaInstrument = parmaInstrument;
            _cE602MInstrument = cE602MInstrument;
            _secondStagePanel = secondStagePanel;
            _firstStageItemPreview = itemPreview;
        }
        public void PrepareToOpen()
        {
            _isOpen = false;
        }
        public void ResetIsWasTaked()
        {
            IsWasTaked = false;
        }

        public void Close()
        {
            _isOpen = false;
            _cE602MInterface.Close();
            _electricClampInstruments.Close();
            _parmaInstrument.Close();
            _cE602MInstrument.Close();
            _itemPreviewPanel.CloseInventoryPanel();
            _gerconInstrument.Close();
            _secondStagePanel.Open();
        }

        public void Switch(Item item)
        {
            if (_isOpen)
            {
                _isOpen = false;
                _cE602MInterface.Close();
                _electricClampInstruments.Close();
                _parmaInstrument.Close();
                _cE602MInstrument.Close();
                _itemPreviewPanel.CloseInventoryPanel();
                _gerconInstrument.Close();
                _secondStagePanel.Open();
            }
            else
            {
                IsWasTaked = true;

                _isOpen = true;
                _electricClampInstruments.Close();
                _parmaInstrument.Close();
                _cE602MInstrument.Close();
                _gerconInstrument.Open();
                _itemPreviewPanel.CloseInventoryPanel();
                _secondStagePanel.Open();
                _firstStageItemPreview.SelectItemInstrument(item);
            }
        }
    }
}