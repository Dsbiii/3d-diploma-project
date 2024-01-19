using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Stages.FourthStage.CablesSystem
{  
    public class FourthStageCablePoint : MonoBehaviour
    {
        [SerializeField] private Transform _boxPoint;
        [SerializeField] private Color _color;
        [SerializeField] private FourthStageCablePoint[] _rightCablePoints;
        [SerializeField] private GameObject _indicator;
        [SerializeField] private CableComponent _cableComponent;
        [SerializeField] private Transform _connectPoint;

        private bool _isConnected;
        public bool IsConnected => _isConnected;
        public FourthStageCablePoint ConnectingCablePoint { get; private set; }
        public IEnumerable<FourthStageCablePoint> RightCablePoints => _rightCablePoints;
        public Transform ConnectPoint => _connectPoint;
        public Transform BoxPoint => _boxPoint;


        private void Awake()
        {
            _indicator.SetActive(false);
        }

        public void ResetConnect()
        {
            _cableComponent.PullOutCable();
            //_isConnected = false;
        }

        public void RightConnect()
        {
            Connect(_rightCablePoints[0]);
        }

        public void Connect(FourthStageCablePoint fourthStageCablePoint)
        {
            _isConnected = true;
            ConnectingCablePoint = fourthStageCablePoint;
            ConnectingCablePoint.ConnectTo(this);
            _cableComponent.ConnectCable(_boxPoint, _color);
        }

        public void ConnectTo(FourthStageCablePoint fourthStageCablePoint)
        {
            _isConnected = true;
            ConnectingCablePoint = fourthStageCablePoint;
            _cableComponent.ConnectCable(_boxPoint, _color);
        }

        public void TryDisplayIndicator()
        {
            if (!_isConnected)
                _indicator.SetActive(true);
        }
        
        public void HideIndicator()
        {
            _indicator.SetActive(false);
        }

    }
}