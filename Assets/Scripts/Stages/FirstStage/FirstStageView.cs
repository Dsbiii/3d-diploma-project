using Assets.Scripts.Menu.Panels;
using Assets.Scripts.Stages.FirstStage.Panels;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FirstStage
{
    public class FirstStageView : MonoBehaviour
    {
        private ItemPreview _firstStageItemPreview;
        private PreviewPanel _previewPanel;
        private InventoryItemPreviewPanel _inventoryItemPreviewPanel;

        public void Init(PreviewPanel previewPanel, ItemPreview firstStageItemPreview, InventoryItemPreviewPanel inventoryItemPreviewPanel)
        {
            _firstStageItemPreview = firstStageItemPreview;
            _inventoryItemPreviewPanel = inventoryItemPreviewPanel;
            _previewPanel = previewPanel;
        }

        public void SelectedInventoryItemHandler(Item item)
        {
            _firstStageItemPreview.PreviewItem(item);
            _inventoryItemPreviewPanel.Open();
        }

        public void SelectedItemHandler(Item item)
        {
            _firstStageItemPreview.PreviewItem(item);
            _previewPanel.Open();
        }
    }
}