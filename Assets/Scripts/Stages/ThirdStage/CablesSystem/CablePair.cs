using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.CablesSystem
{
    [System.Serializable]
    public class CablePair
    {
        [SerializeField] private CablePoint _firstCablePoint;
        [SerializeField] private CablePoint _secondCablePoint;

        public CablePoint FirstCablePoint => _firstCablePoint;
        public CablePoint SecondCablePoint => _secondCablePoint;

        public void SetCablePair(CablePoint firstCablePoint, CablePoint secondCablePoint)
        {
            _firstCablePoint = firstCablePoint;
            _secondCablePoint = secondCablePoint;
        }

        public bool CheckToConnect()
        {
            if  (_firstCablePoint.IsConnected && _firstCablePoint.ConnectingCablePoint == _secondCablePoint)
            {
                return true;
            }

            if (_secondCablePoint.IsConnected && _secondCablePoint.ConnectingCablePoint == _firstCablePoint)
            {
                return true;
            }

            return false;
        }
    }
}