using Assets.Scripts.Stages.FirstStage;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.ThirdStage.Panels
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _exitButton;

        private GameState _firstStageState;
        private ThirdStagePanel _thirdStagePanel;

        private void Awake()
        {
            _exitButton.onClick.AddListener(ExitFromInventory);
        }

        public void Init(GameState firstStageState, ThirdStagePanel firstStagePanel)
        {
            _firstStageState = firstStageState;
            _thirdStagePanel = firstStagePanel;

        }

        public void ExitFromInventory()
        {
            _firstStageState.EnterInInspection();
            _thirdStagePanel.Open();
            Close();
        }

        public void Open()
        {
            _firstStageState.EnterInInventoryState();
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }
    }
}