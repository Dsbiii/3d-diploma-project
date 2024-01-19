using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Stages.FirstStage;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface
{
    [System.Serializable]
    public class MagnitePointPair
    {
        [SerializeField] private MagnitePoint[] _firstMagnitePoints;
        [SerializeField] private MagnitePoint _secondMagnitePoint;

        public bool CheckIsConnect()
        {

            foreach(var item in _firstMagnitePoints)
            {
                if (item.IsMagnited && _secondMagnitePoint.IsMagnited &&
                    _secondMagnitePoint.MagnitedPointsType == item.MagnitedPointsType)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class CE602MInterface : MonoBehaviour
    {
        [SerializeField] private MagnitePointPair[] _magnitePointPairs;
        [SerializeField] private GameObject _wrongPanel;
        [SerializeField] private GameObject[] _rightPositionItems;
        [SerializeField] private GameObject[] _kleshi;
        [SerializeField] private GameObject[] _priborObjects;
        [SerializeField] private CEView _cEView;
        [SerializeField] private GameState _gameState;
        [SerializeField] private LayerMask _layerMask;

        public bool IsFirstableRightConnect { get; private set; } = true;
        public bool IsOpenFirst { get; private set; }

        private bool _isActive;
        private bool _isOpen;

        public void RessetUsingCE602M()
        {
            IsFirstableRightConnect = true;
            IsOpenFirst = false;
        }

        public void Active()
        {
            _isActive = true;
        }

        public void Disable()
        {
            _isActive = false;
        }

        private void Update()
        {
            if (!_isActive)
                return;

            if (Input.GetMouseButtonDown(0) && !_isOpen)
            {
                if (TryPickCE602M())
                {
                    IsOpenFirst = true;
                    _wrongPanel.SetActive(false);
                    foreach (var item in _priborObjects)
                        item.SetActive(true);
                    _cEView.OpenView();
                    _isOpen = true;
                }

                //if (TryPickCE602M() && _kleshi.Where(item => !item.activeSelf).ToArray().Length == _kleshi.Length)
                //{
                //    if (!CheckToRightComlete() || !CheckToRightItemsSetups())
                //    {
                //        IsFirstableRightConnect = false;
                //        _wrongPanel.SetActive(true);
                //        return;
                //    }
                //    if (CheckToOpenCE602M())
                //    {
                //        IsOpenFirst = true;
                //        _wrongPanel.SetActive(false);
                //        foreach (var item in _priborObjects)
                //            item.SetActive(true);
                //        _cEView.OpenView();
                //        _isOpen = true;
                //    }
                //}
            }
            if (_isOpen)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    foreach (var item in _priborObjects)
                        item.SetActive(false);
                    _isOpen = false;
                    _gameState.EnterInInspection();
                    _cEView.CloseView();
                }
            }
        }

        public void HideWrongPanelText()
        {
            _wrongPanel.SetActive(false);
        }

        private bool CheckToRightComlete()
        {
            if (_magnitePointPairs.Where(item => item.CheckIsConnect()).ToArray().Length >= _magnitePointPairs.Length)
            {
                return true;
            }
            return false;
        }


        private bool CheckToRightItemsSetups()
        {
            if (_rightPositionItems.Where(item => item.activeSelf).ToArray().Length >= _rightPositionItems.Length)
            {
                return true;
            }
            return false;
        }

        public void Close()
        {
            _wrongPanel.SetActive(false);
            _isOpen = false;
            _cEView.CloseView();
        }

        private bool CheckToOpenCE602M()
        {
            if(_kleshi.Where(item => !item.activeSelf).ToArray().Length >= _kleshi.Length)
            {
                return true;
            }
            return false;
        }

        public bool TryPickCE602M()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _layerMask))
            {
                if (hit.transform != null)
                    return true;

            }
            return false;
        }
    }
}