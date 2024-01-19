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
    public class ParmaService : InstrumentService
    {
        private MeteringParma _meteringParma;
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
        public bool IsWasUssed { get; private set; }
        public bool IsBackedInBag { get; private set; }

        public ParmaService(MeteringParma meteringParma,ResetButtonService resetButtonService ,GerconInstrument gerconInstrument,ItemPreviewPanel itemPreviewPanel,ElectricClampInstruments electricClampInstruments, ParmaInstrument parmaInstrument, CE602MInstrument cE602MInstrument,
                SecondStagePanel secondStagePanel, ItemPreview itemPreview)
        {
            _meteringParma = meteringParma;
            _resetButtonService = resetButtonService;
            _gerconInstrument = gerconInstrument;
            _itemPreviewPanel = itemPreviewPanel;
            _electricClampInstruments = electricClampInstruments;
            _parmaInstrument = parmaInstrument;
            _cE602MInstrument = cE602MInstrument;
            _secondStagePanel = secondStagePanel;
            _firstStageItemPreview = itemPreview;
        }
        public void ResetIsWasTaked()
        {
            IsWasTaked = false;
        }


        public void PrepareToOpen()
        {
            _isOpen = false;
        }

        public override void Close()
        {
            IsBackedInBag = true;

            _resetButtonService.HideButton();
            _isOpen = false;
            _gerconInstrument.Close();
            _electricClampInstruments.Close();
            _cE602MInstrument.Close();
            _parmaInstrument.Close();
            _itemPreviewPanel.CloseInventoryPanel();
            _secondStagePanel.Open();
        }
        public void Switch(Item item)
        {
            if (_isOpen)
            {
                _resetButtonService.HideButton();
                _isOpen = false;
                _gerconInstrument.Close();
                _electricClampInstruments.Close();
                _cE602MInstrument.Close();
                _parmaInstrument.Close();
                _itemPreviewPanel.CloseInventoryPanel();
                _secondStagePanel.Open();
                IsBackedInBag = true;
            }
            else
            {
                _resetButtonService.SetCurrentInstrumentService(this);
                _resetButtonService.DisplayButton();
                IsWasTaked = true;
                _gerconInstrument.Close();
                _electricClampInstruments.Close();
                _cE602MInstrument.Close();
                _parmaInstrument.Open();
                _itemPreviewPanel.CloseInventoryPanel();
                _secondStagePanel.Open();
                _firstStageItemPreview.SelectItemInstrument(item);
                _isOpen = true;
            }
        }

 

        public override void Open()
        {
        }

        public override void ResetInstruemnt()
        {
            _meteringParma.ResetParma();
            _parmaInstrument.ResetInstruemnt();
        }
    }
}