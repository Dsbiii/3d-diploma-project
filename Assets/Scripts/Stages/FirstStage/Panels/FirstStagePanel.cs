using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FirstStage.Panels
{
    public class FirstStagePanel : Panel
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Button _compliteButton;

        private InventoryPanel _inventoryPanel;
        private FirstStageController _firstStageController;

        private void Awake()
        {
            _inventoryButton.onClick.AddListener(OpenInventory);
            _compliteButton.onClick.AddListener(Complite);
        }

        public void Init(InventoryPanel inventoryPanel, FirstStageController firstStageController)
        {
            _inventoryPanel = inventoryPanel;
            _firstStageController = firstStageController;
        }

        public void Complite()
        {
            _firstStageController.Complite();
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