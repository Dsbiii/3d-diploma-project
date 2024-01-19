using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.ThirdStage.Panels
{
    public class ThirdStagePanel : Panel
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Button _setupButton;

        private InventoryPanel _inventoryPanel;
        private SetupPanel _setupPanel;

        private void Awake()
        {
            _inventoryButton.onClick.AddListener(OpenInventory);
        }

        public void Init(InventoryPanel inventoryPanel, SetupPanel setupPanel)
        {
            _inventoryPanel = inventoryPanel;
            _setupPanel = setupPanel;
        }

        public void Setup()
        {
            _setupPanel.Open();
        }

        public void OpenInventory()
        {
            _inventoryPanel.Open();
            Close();
        }

        public override void Open()
        {
            _panel.SetActive(true);
            base.Open();
        }

        public override void Close()
        {
            _panel.SetActive(false);
        }
    }
}