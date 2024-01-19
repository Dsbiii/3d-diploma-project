using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FirstStage.Panels
{
    public class PreviewPanel : Panel
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _close;
        [SerializeField] private Button _bagButton;
        [SerializeField] private Button _replace;

        private FirstStageController _firstStageController;
        private ItemPreview _firstStageItemPreview;
        private FirstStagePanel _firstStagePanel;
        public bool IsOpen { get; private set; }
        private void Awake()
        {
            _close.onClick.AddListener(Close);
            _bagButton.onClick.AddListener(AddToInventory);
            _replace.onClick.AddListener(Replace);
        }

        public void Init(ItemPreview firstStageItemPreview , FirstStageController firstStageController , FirstStagePanel firstStagePanel)
        {
            _firstStagePanel = firstStagePanel;
            _firstStageItemPreview = firstStageItemPreview;
            _firstStageController = firstStageController;
        }

        public void Replace()
        {
            _firstStageItemPreview.SelectedItem.Refresh();
        }

        public void AddToInventory()
        {
            _firstStageController.AddItemInInventory();
            _firstStageController.ExitFromPreview();
            _firstStageItemPreview.BackFromPreview();
            _firstStagePanel.Open();
            IsOpen = false;
            _panel.SetActive(false);
        }

        public override void Open()
        {
            _firstStagePanel.Close();
            _panel.SetActive(true);
            IsOpen = true;
            base.Open();
        }

        public override void Close()
        {
            IsOpen = false;
            _firstStageController.ExitFromPreview();
            _firstStageItemPreview.BackFromPreview();
            _firstStagePanel.Open();
            _panel.SetActive(false);
        }
    }
}