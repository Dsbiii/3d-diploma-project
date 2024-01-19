using Assets.Scripts.Menu.Panels;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.FirstStage.Panels;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.CompositeRoot
{
    public class FirstStageCompositeRoot : CompositeRoot
    {
        [SerializeField] private ItemsInventory _itemsInventory;
        [SerializeField] private InventoryItemPreviewPanel _inventoryItemPreviewPanel;
        [SerializeField] private InventoryPanel _inventoryPanel;
        [SerializeField] private FirstStagePanel _firstStagePanel;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private PreviewPanel _previewPanel;
        [SerializeField] private FirstStageController _firstStageController;
        [SerializeField] private FirstStageItemPicker _firstStageItemPicker;
        [SerializeField] private ItemPreview _firstStageItemPreview;
        [SerializeField] private GameState _firstStageState;
        [SerializeField] private FirstStageView _firstStageView;

        private FirstStageModel _firstStageModel;

        public override void Composite()
        {
            _firstStageModel = new FirstStageModel();
            _inventory.Init(_inventoryView);
            _inventoryItemPreviewPanel.Init(_inventoryPanel,_firstStageItemPreview, _firstStageController);
            _firstStagePanel.Init(_inventoryPanel, _firstStageController);
            _inventoryPanel.Init(_firstStageState,_firstStagePanel);
            _previewPanel.Init(_firstStageItemPreview, _firstStageController, _firstStagePanel);
            _firstStageController.Init(_previewPanel, _inventoryItemPreviewPanel,_itemsInventory,_firstStageItemPicker, _firstStageState, _firstStageModel, _inventory);
            _firstStageItemPicker.Init(_firstStageItemPreview);
            _firstStageItemPreview.Init(_firstStageState);
            _firstStageView.Init(_previewPanel, _firstStageItemPreview,_inventoryItemPreviewPanel);

            _firstStageModel.OnSelectedItem += _firstStageView.SelectedItemHandler;
            _firstStageModel.OnSelectedInventoryItem += _firstStageView.SelectedInventoryItemHandler;
        }
    }
}