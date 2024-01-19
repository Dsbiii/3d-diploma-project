using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.FirstStage.Panels;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Panels
{
    public class InventoryItemPreviewPanel : Panel
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _close;
        [SerializeField] private Button _layOutFromBagButton;
        [SerializeField] private Button _replace;

        private InventoryPanel _inventoryPanel;
        private FirstStageController _firstStageController;
        private ItemPreview _firstStageItemPreview;
        public bool IsOpen { get; private set; }

        private void Awake()
        {
            _close.onClick.AddListener(Close);
            _layOutFromBagButton.onClick.AddListener(RemoveFromInventory);
        }

        public void Init(InventoryPanel inventoryPanel,ItemPreview firstStageItemPreview, FirstStageController firstStageController)
        {
            _inventoryPanel = inventoryPanel;
            _firstStageItemPreview = firstStageItemPreview;
            _firstStageController = firstStageController;
        }

        public void RemoveFromInventory()
        {
            _firstStageController.RemoveItemFromInventory();
            IsOpen = false;
            _inventoryPanel.Open();
            _firstStageController.ExitFromInventoryPreview();
            _firstStageItemPreview.BackFromPreview();
            _panel.SetActive(false);
        }

        public override void Open()
        {
            _inventoryPanel.Close();
            _panel.SetActive(true);
            IsOpen = true;
            base.Open();
        }

        public override void Close()
        {
            IsOpen = false;
            _inventoryPanel.Open();
            _firstStageController.ExitFromInventoryPreview();
            _firstStageItemPreview.BackFromPreview(true);
            _panel.SetActive(false);
        }
    }
}