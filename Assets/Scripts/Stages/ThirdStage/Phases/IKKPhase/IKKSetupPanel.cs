using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.ThirdStage.Phases.IKKPhase
{
    public class IKKSetupPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _ikk;
        [SerializeField] private ItemTransferService _itemTransferService;
        [SerializeField] private IKKPoint[] _iKKPoints;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _plantButton;

        private IKKPhase _ikkPhase;
        private ThirdStageModel _stageModel;

        private void Awake()
        {
            _plantButton.onClick.AddListener(Plant);
        }

        public void Init(IKKPhase iKKPhase, ThirdStageModel thirdStageModel)
        {
            _stageModel = thirdStageModel;
            _ikkPhase = iKKPhase;
        }

        public void DisplayPoints()
        {
            foreach (var item in _iKKPoints)
                item.TryDisplayPoint();
        }

        public void Plant()
        {
            _ikkPhase.Plant();
            foreach (var item in _iKKPoints)
                item.OffPoint();
            _itemTransferService.UndoCurrentItem();
            Close();
            _stageModel.PlantedIKK();
            _ikk.SetActive(true);
        }

        public void Back()
        {
            _ikkPhase.BackToInventory();
            Close();
        }

        public void Open()
        {
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }

    }
}