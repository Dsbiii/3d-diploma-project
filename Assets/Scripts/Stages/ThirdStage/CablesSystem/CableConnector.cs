using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Instruments;

namespace Assets.Scripts.Stages.ThirdStage.CablesSystem
{
    public class CableConnector : MonoBehaviour
    {
        private CE602MService _cE602MService;
        private ParmaService _parmaService;
        public static CableConnector Instance;
        [SerializeField] private GameObject _cableInfoPanel;
        [SerializeField] private Vector3 _arrowOffset = new Vector3(0.1f, 0, 0);
        [SerializeField] private GameObject _arrow;
        [SerializeField] private List<CablePair> _cablePairs;
        [SerializeField] private CablePanel _cablePanel;
        [SerializeField] private List<CablePoint> _cablePoints;
        [SerializeField] private LayerMask _cableLayer;
        [SerializeField] private LineRenderer _cableLine;
        private CablePoint _firstCablePoint;
        private CablePoint _secondCablePoint;
        private bool _isActive;
        private Vector3 _previousPosition;

        private void Awake()
        {
            _cableLine.positionCount = 2;
            Instance = GetComponent<CableConnector>();
        }

        public void Init(CE602MService cE602MService , ParmaService parmaService)
        {
            _cE602MService = cE602MService;
            _parmaService = parmaService;
        }

        public void Deactive()
        {
            _isActive = false;

        }

        public void Active()
        {
            _isActive = true;
            foreach (var item in _cablePoints)
                item.gameObject.SetActive(true);
            foreach (var item in _cablePoints)
                if (!item.IsConnected)
                    item.DisplayIndicator();

            foreach (var item in _cablePoints)
                item.DisplayIndicator();
        }

        public bool CheckToRightConnection()
        {

            if (_cablePairs.Where(item => item.CheckToConnect()).ToArray().Length >= _cablePairs.Count)
            {
                return true;
            }
            return false;
        }

        public void CompleteCables()
        {
            foreach (var item in _cablePoints)
                item.Complete();
        }

        public bool TryGetCablePointInMousePosition(out CablePoint cablePoint)
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
                if (!item.IsConnected || item.IsAlwaysIndicate)
                    item.DisplayIndicator();
        }

        public void HideIndicatorsCables()
        {

            foreach (var item in _cablePoints)
                if (!item.IsConnected)
                    item.HideIndicator();
        }

        public void HideAllIndicatorsCables()
        {

            foreach (var item in _cablePoints)
                    item.HideIndicator();
        }

        public void SetupCables()
        {
            _cableLine.positionCount = 0;
            _cableInfoPanel.SetActive(false);
            if (_firstCablePoint.IsResusablePoint)
            {
                _secondCablePoint.Connect(_firstCablePoint);

            }
            else
            {
                _firstCablePoint.Connect(_secondCablePoint);

            }
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

        public void Update()
        {
            if (_parmaService.IsOpen)
                return;

            if (_cE602MService.IsOpen)
                return;

            if (!_isActive)
                return;
           
            DrawConnectingCable();
            if (Input.GetMouseButtonDown(0))
            {
                if(TryGetCablePointInMousePosition(out CablePoint cablePoint) && _secondCablePoint == null)
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
                if (TryGetCablePointInMousePosition(out CablePoint cablePoint) && _firstCablePoint != null)
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
                        _arrow.SetActive(false);
                        DisplayIndicatorsCables();
                        return;
                    }

                    if (_firstCablePoint != null && _firstCablePoint == cablePoint)
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
                        _arrow.SetActive(false);
                        DisplayIndicatorsCables();
                        return;
                    }

                    _secondCablePoint = cablePoint;
                    HideIndicatorsCables();
                    _cablePanel.Open();
  
                }
                else
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
                    _arrow.SetActive(false);
                    DisplayIndicatorsCables();
                }
            }
        }

    }
}