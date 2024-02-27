using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Instruments;

namespace Assets.Scripts.Stages.FourthStage.CablesSystem
{
    public class FourthStageCableConnector : MonoBehaviour
    {
        [SerializeField] private GameObject _cableInfoPanel;
        [SerializeField] private List<FourthStageCablePair> _powerAutomatBlockCablePairs;
        [SerializeField] private List<FourthStageCablePair> _powerBlockUSPDcablePairs;
        [SerializeField] private List<FourthStageCablePair> _counterUSPDcablePairs;

        [SerializeField] private FourthStageCablePanel _cablePanel;
        [SerializeField] private List<FourthStageCablePoint> _cablePoints;
        [SerializeField] private LayerMask _cableLayer;
        [SerializeField] private LineRenderer _cableLine;
        private FourthStageCablePoint _firstCablePoint;
        private FourthStageCablePoint _secondCablePoint;
        private bool _isActive;

        public void Active()
        {
            _isActive = true;
            foreach (var item in _cablePoints)
                item.gameObject.SetActive(true);
            foreach (var item in _cablePoints)
                if (!item.IsConnected)
                    item.TryDisplayIndicator();
            foreach (var item in _cablePoints)
                item.TryDisplayIndicator();
        }

        public bool CheckToRightConnection()
        {
            List<FourthStageCablePair> pairs = _powerAutomatBlockCablePairs.Union(_powerBlockUSPDcablePairs).ToList();
            if (pairs.Where(item => item.CheckToConnect()).ToArray().Length >= pairs.Count
                && _counterUSPDcablePairs.Where(item => item.CheckToConnect()).ToArray().Length  + 2 >= _counterUSPDcablePairs.Count / 2)
            {
                return true;
            }
            return false;
        }


        public bool TryGetCablePointInMousePosition(out FourthStageCablePoint cablePoint)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _cableLayer))
            {
                if (hit.transform.TryGetComponent(out cablePoint))
                {
                    return true;
                }
            }
            cablePoint = null;
            return false;
        }

        public void DrawConnectingCable()
        {

            if (_secondCablePoint != null)
            {
                _cableLine.SetPosition(1, _secondCablePoint.transform.position);

                return;
            }
            if(_firstCablePoint != null)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
                {
                    _cableLine.SetPosition(1, hit.point);
                }
            }
        }

        public void DisplayIndicatorsCables()
        {
            foreach (var item in _cablePoints)
                item.TryDisplayIndicator();
        }

        public void HideIndicatorsCables()
        {
            foreach (var item in _cablePoints)
                    item.HideIndicator();
        }

        public void SetupCables()
        {
            _cableLine.positionCount = 0;
            _cableInfoPanel.SetActive(false);
            _firstCablePoint.Connect(_secondCablePoint);
            _firstCablePoint = null;
            _secondCablePoint = null;
            DisplayIndicatorsCables();
        }

        public void PutAwayCables()
        {
            _cableLine.positionCount = 0;
            _firstCablePoint = null;
            _secondCablePoint = null;
            DisplayIndicatorsCables();
        }

        public void ResetConnectionConnectAllCables()
        {
            //foreach(var item in _cablePoints)
            //{
            //    item.gameObject.SetActive(true);
            //    item.ResetConnect();
            //}
            foreach(var item in _cablePoints)
            {
                item.ResetConnect();
                item.gameObject.SetActive(false);
            }
            //_counterUSPDcablePairs[0].FirstCablePoint.RightConnect();
            //_counterUSPDcablePairs[0].SecondCablePoint.RightConnect();
            //_counterUSPDcablePairs[2].FirstCablePoint.RightConnect();
            //_counterUSPDcablePairs[2].SecondCablePoint.RightConnect();
            ////ConnectRightPoints(_counterUSPDcablePairs);
            //ConnectRightPoints(_powerAutomatBlockCablePairs);
            //ConnectRightPoints(_powerBlockUSPDcablePairs);
        }

        private void ConnectRightPoints(IEnumerable<FourthStageCablePair> fourthStageCablePoints)
        {
            foreach (var item in fourthStageCablePoints)
            {
                item.FirstCablePoint.RightConnect();
                item.SecondCablePoint.RightConnect();
            }
        }

        public void Update()
        {
            if (!_isActive)
                return;
           
            DrawConnectingCable();


            if (Input.GetMouseButtonDown(0))
            {
                if (TryGetCablePointInMousePosition(out FourthStageCablePoint cablePoint) && _secondCablePoint == null)
                {
                    if (cablePoint.IsConnected)
                        return;
                    _cableInfoPanel.SetActive(true);
                    _cableLine.positionCount = 2;
                    HideIndicatorsCables();
                    _firstCablePoint = cablePoint;
                    _cableLine.SetPosition(0, _firstCablePoint.transform.position);
                    DisplayIndicatorsCables();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (TryGetCablePointInMousePosition(out FourthStageCablePoint cablePoint) && _firstCablePoint != null)
                {
                    if (cablePoint.IsConnected)
                    {

                        if (_firstCablePoint != null && _secondCablePoint == null)
                        {
                            HideIndicatorsCables();
                            _firstCablePoint = null;
                            _cableLine.positionCount = 0;
                        }
                        if (_secondCablePoint != null)
                        {
                            HideIndicatorsCables();
                            _firstCablePoint = null;
                            _secondCablePoint = null;
                            _cableLine.positionCount = 0;
                        }
                        _cableInfoPanel.SetActive(false);
                        _cablePanel.Close();
                        DisplayIndicatorsCables();
                        return;
                    }

                    if (_firstCablePoint != null && _firstCablePoint == cablePoint)
                    {
                        HideIndicatorsCables();
                        _firstCablePoint = null;
                        _secondCablePoint = null;
                        _cableLine.positionCount = 0;
                        _cableInfoPanel.SetActive(false);
                        _cablePanel.Close();
                        DisplayIndicatorsCables();
                        return;
                    }
                    _secondCablePoint = cablePoint;
                    HideIndicatorsCables();
                    _cablePanel.Open();
                }
                else
                {
                    HideIndicatorsCables();
                    _firstCablePoint = null;
                    _secondCablePoint = null;
                    _cableLine.positionCount = 0;
                    _cableInfoPanel.SetActive(false);
                    _cablePanel.Close();
                    DisplayIndicatorsCables();
                }
            }
        }

    }
}