using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Stages.ThirdStage.CablesSystem
{  
    public class CablePoint : MonoBehaviour
    {
        [SerializeField] private bool _isAlwaysIndicate;
        [SerializeField] private bool _isEarthTransformator;
        [SerializeField] private bool _isColored;
        [SerializeField] private GameObject _earthTransformatorCable;
        [SerializeField] private Color _color;
        [SerializeField] private CablePoint _rightCablePoint;
        [SerializeField] private bool _isEarth;
        [SerializeField] private List<Transform> _points;
        [SerializeField] private bool _reusablePoint;
        [SerializeField] private CablePoint[] _suitableCablePoints;
        [SerializeField] private GameObject _indicator;
        [SerializeField] private GameObject[] _cableObjects;
        [SerializeField] private CableComponent _cableComponent;

        private bool _isConnected;

        public bool IsAlwaysIndicate => _isAlwaysIndicate;
        public GameObject EarthTransformatorCable => _earthTransformatorCable;
        public bool IsConnected => _isConnected;
        public bool IsEarthTransformator => _isEarthTransformator;
        public bool IsResusablePoint => _reusablePoint;
        public bool IsEarth => _isEarth;
        public CablePoint ConnectingCablePoint { get; private set; }
        public CablePoint RightCablePoint => _rightCablePoint;
        public IReadOnlyList<CablePoint> SuitableCablePoints => _suitableCablePoints;
        public Color Color => _color;
        public bool IsCollored => _isColored;


        public Transform Point
        {
            get
            {
                foreach (var item in _points)
                    if (item.gameObject.activeSelf)
                        return item;
                return _points[0];

            }
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        private void Awake()
        {
            _indicator.SetActive(false);
        }

        public void SetRightCablePoint(CablePoint cablePoint)
        {
            _rightCablePoint = cablePoint;
        }

        public void Complete()
        {
            _isConnected = true;
            HideIndicator();
        }

        public void Update()
        {
            if(IsResusablePoint && !_isConnected && !IsEarth)
            {
                if(_points.Where(item => !item.gameObject.activeSelf).ToArray().Length >= _points.Count)
                {
                    Complete();
                }
            }
        }

        public void Connect(CablePoint cablePoint)
        {
            Transform point = cablePoint.Point;
            ConnectingCablePoint = cablePoint;
            point.gameObject.SetActive(false);
            if (cablePoint.EarthTransformatorCable != null)
            {
                cablePoint.Connected(false, false);
                Connected(false, false);
                cablePoint.EarthTransformatorCable.SetActive(true);
                return;
            }


            if (EarthTransformatorCable != null)
            {
                cablePoint.Connected(false, false);
                Connected(false, false);
                cablePoint.EarthTransformatorCable.SetActive(true);
                return;
            }

            if (cablePoint.IsEarth)
            {
                cablePoint.Connected(false, false);
                Connected(false);
            }
            else if(IsEarth)
            {
                cablePoint.Connected(false);
                Connected(false, false);
            }
            else if (IsEarthTransformator)
            {
                cablePoint.Connected(false);
                //_isConnected = true;
                _earthTransformatorCable.SetActive(true);
            }
            else
            {
                cablePoint.Connected();
                Connected();
                if (cablePoint._isColored)
                {
                    _cableComponent.ConnectCable(point, cablePoint.Color);
                }
                else
                {
                    _cableComponent.ConnectCable(point, _color);
                }
                //if (cablePoint._isColored)
                //{
                //    _cableComponent.ConnectCable(point, cablePoint.Color);
                //}
                //else
                //{
                //    _cableComponent.ConnectCable(point, _color);
                //}
            }
        }

        public void DisplaySuitableCablePoints()
        {
            foreach(var cable in _suitableCablePoints)
                if(cable != this && !cable.IsConnected)
                    cable.DisplayIndicator();
        }

        public void HideSutableCablePoints()
        {
            foreach (var cable in _suitableCablePoints)
                if (cable != this && !cable.IsConnected)
                    cable.HideIndicator();
        }

        public void DisplayIndicator()
        {
            _indicator.SetActive(true);
        }

        public void HideIndicator()
        {
            _indicator.SetActive(false);
        }

        public void Connected(bool isOnCableObjects = true , bool isConnect = true)
        {
            if (!IsEarth)
            {
                _isConnected = true;
            }

            if (isConnect)
            {
                _isConnected = true;
            }

            //if (!IsResusablePoint)
            //    _isConnected = true;
            //if (IsEarth)
            //    _isConnected = true;

            if (isOnCableObjects)
            {
                foreach (var item in _cableObjects)
                {
                    item.SetActive(true);
                    if (item.TryGetComponent(out MeshRenderer meshRenderer))
                        meshRenderer.enabled = true;
                }
            }
            HideIndicator();
        }

    }
}