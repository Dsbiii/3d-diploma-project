using Assets.Scripts.Stages.FirstStage;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SecondStage.Panels
{
    public class InventoryPanel : Panel
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _exitButton;

        private SecondStagePanel _secondStagePanel;
        private GameState _secondStageState;

        private void Awake()
        {
            _exitButton.onClick.AddListener(ExitFromInventory);
        }

        public void Init(SecondStagePanel secondStagePanel, GameState secondStageState)
        {
            _secondStageState = secondStageState;
            _secondStagePanel = secondStagePanel;
        }

        public void ExitFromInventory()
        {
            _secondStagePanel.Open();
            Close();
        }

        public override void Open()
        {
            _secondStageState.EnterInInventoryState();
            _panel.SetActive(true);
            base.Open();
        }

        public override void Close()
        {
            _panel.SetActive(false);
        }
    }
}