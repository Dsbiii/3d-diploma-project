using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FirstStage.Panels
{
    public class InventoryPanel : Panel
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _exitButton;

        private FirstStagePanel _firstStagePanel;
        private GameState _firstStageState;

        private void Awake()
        {
            _exitButton.onClick.AddListener(ExitFromInventory);
        }

        public void Init(GameState firstStageState ,FirstStagePanel firstStagePanel)
        {
            _firstStageState = firstStageState;
            _firstStagePanel = firstStagePanel;

        }

        public void ExitFromInventory()
        {
            _firstStageState.EnterInSelectingItemState();
            _firstStagePanel.Open();
            Close();
        }

        public override void Open()
        {
            _firstStageState.EnterInInventoryState();
            _panel.SetActive(true);
            base.Open();
        }

        public override void Close()
        {
            _panel.SetActive(false);
        }
    }
}