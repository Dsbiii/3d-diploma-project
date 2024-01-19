using Assets.Scripts.Stages.SecondStage.Dismantling;
using Assets.Scripts.Stages.SecondStage.Dismantling.Install_screw;
using Assets.Scripts.Stages.SecondStage.Electric_Box;
using Assets.Scripts.Stages.ThirdStage.Objects.Counter;
using Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.ThirdStage.Phases.IKKPhase
{
    public class IKKPhase : MonoBehaviour, ITransferHandler
    {
        [SerializeField] private IKK _iKK;
        [SerializeField] private ItemTransferService _itemTransferService;
        [SerializeField] private LayerMask _pointLayer;
        [SerializeField] private IKKTS _ikkWorkVersion;
        [SerializeField] private Screw[] _screws;

        private Inventory _inventory;
        private Item _currentItem;
        private IKKSetupPanel _iKKSetupPanel;
        private CablesConnector _cablesConnector;

        public bool IsPlanted { get; private set; }

        public void Init(CablesConnector cablesConnector,Inventory inventory, IKKSetupPanel iKKSetupPanel)
        {
            _cablesConnector = cablesConnector;
            _inventory = inventory;
            _iKKSetupPanel = iKKSetupPanel;
        }

        public void CloseScreews()
        {
            foreach (var item in _screws)
                item.Close();
        }

        public void Plant()
        {
            foreach (var item in _screws)
                item.Open();
            _ikkWorkVersion.gameObject.SetActive(true);
            _ikkWorkVersion.transform.position = ((IKKTS)_currentItem).transform.position - new Vector3(0.1f,0,0);
            _ikkWorkVersion.transform.rotation = ((IKKTS)_currentItem).transform.rotation;
            ((IKKTS)_currentItem).gameObject.SetActive(false);
            _itemTransferService.UndoCurrentItem();
            IsPlanted = true;
            _iKK.OpenWithOutScrewDriver();
        }

        public void BackToInventory()
        {
            _inventory.AddItem(_currentItem);
            _currentItem.gameObject.SetActive(false);
        }

        public void EndTransferItem(Item item)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity,_pointLayer))
            {
                if(hit.transform.TryGetComponent(out IKKPoint iKKPoint))
                {
                    _currentItem = item;
                    _iKKSetupPanel.Open();
                }
                else
                {
                    _iKKSetupPanel.Close();
                }
            }
            else
            {
                _iKKSetupPanel.Close();
            }
        }

        public bool IsPlaced()
        {
            throw new System.NotImplementedException();
        }
    }
}