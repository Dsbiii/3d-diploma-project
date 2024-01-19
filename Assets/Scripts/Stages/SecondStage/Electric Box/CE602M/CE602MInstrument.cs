using Assets.Scripts.Instruments;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M
{
    public class CE602MInstrument : MonoBehaviour
    {
        [SerializeField] private GameObject _ikkPoints;
        [SerializeField] private CE602M _cE602M;
        [SerializeField] private GameObject _pribor;
        [SerializeField] private ItemPreview _itemPreview;
        [SerializeField] private MovebleObject[] _movebleObjects;
        [SerializeField] private CE602MInterface _cE602MInterface;
        [SerializeField] private GameObject[] _elemnts;
        private bool _isOpen;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _isOpen)
                Close();
            if (Input.GetMouseButtonDown(2) && _isOpen)
            {
                _cE602MInterface.Close();
                _pribor.SetActive(false);
            }
        }

        public void ResetMovebleObjects()
        {
            foreach (var movebleObjects in _movebleObjects)
                movebleObjects.BackInStorage();
        }

        public void Open()
        {
            _ikkPoints.SetActive(true);
               _isOpen = true;
            foreach (var item in _elemnts)
            {
                item.SetActive(true);
            }
        }

        public void Close()
        {
            _ikkPoints.SetActive(false);
            _isOpen = false;
            _cE602M.OffActivePoints();
            _itemPreview.HideCurrentInstrumentItem();
            ResetMovebleObjects();
            _pribor.SetActive(false);
            foreach (var item in _elemnts)
            {
                item.SetActive(false);
            }
        }

        public void ResetInstruemnt()
        {
            _cE602MInterface.HideWrongPanelText();
            ResetMovebleObjects();
        }
    }
}