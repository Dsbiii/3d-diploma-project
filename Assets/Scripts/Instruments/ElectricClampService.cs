using Assets.Scripts.Stages;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.SecondStage.Dismantling;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M;
using Assets.Scripts.Stages.SecondStage.Electric_Box.Parma;
using Assets.Scripts.Stages.SecondStage.Panels;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Instruments
{
    public class ElectricClampService : InstrumentService
    {
        private ResetButtonService _resetButtonService;
        private ElectricClampInstruments _electricClampInstruments;
        private ParmaInstrument _parmaInstrument;
        private CE602MInstrument _cE602MInstrument;
        private ItemPreviewPanel _itemPreviewPanel;
        private SecondStagePanel _secondStagePanel;
        private ItemPreview _firstStageItemPreview;
        private GerconInstrument _gerconInstrument;
        private bool _isOpen;

        public bool IsOpen => _isOpen;
        public bool IsWasTaked { get; private set; }


        public ElectricClampService(ResetButtonService resetButtonService,GerconInstrument gerconInstrument,ItemPreviewPanel itemPreviewPanel, ElectricClampInstruments electricClampInstruments, ParmaInstrument parmaInstrument,
            CE602MInstrument cE602MInstrument,SecondStagePanel secondStagePanel, ItemPreview itemPreview)
        {
            _resetButtonService = resetButtonService;
            _gerconInstrument = gerconInstrument;
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

        public override void Close()
        {
            _isOpen = false;
            _parmaInstrument.Close();
            _cE602MInstrument.Close();
            _electricClampInstruments.Close();
            _itemPreviewPanel.CloseInventoryPanel();
            _gerconInstrument.Close();
            _secondStagePanel.Open();
            _resetButtonService.HideButton();
        }

        public void Switch(Item item)
        {
            if (_isOpen)
            {
                _isOpen = false;
                _parmaInstrument.Close();
                _cE602MInstrument.Close();
                _electricClampInstruments.Close();
                _itemPreviewPanel.CloseInventoryPanel();
                _gerconInstrument.Close();
                _secondStagePanel.Open();
                _resetButtonService.HideButton();
            }
            else
            {
                IsWasTaked = true;
                _resetButtonService.SetCurrentInstrumentService(this);
                _resetButtonService.DisplayButton();
                _isOpen = true;
                _gerconInstrument.Close();
                _parmaInstrument.Close();
                _cE602MInstrument.Close();
                _electricClampInstruments.Open();
                _itemPreviewPanel.CloseInventoryPanel();
                _secondStagePanel.Open();
                _firstStageItemPreview.SelectItemInstrument(item);
            }
        }

        public override void Open()
        {

        }

        public override void ResetInstruemnt()
        {
            _electricClampInstruments.ResetInstrument();
        }
    }
}