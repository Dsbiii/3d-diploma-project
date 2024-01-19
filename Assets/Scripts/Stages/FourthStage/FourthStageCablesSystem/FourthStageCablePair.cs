using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FourthStage.CablesSystem
{
    [System.Serializable]
    public class FourthStageCablePair
    {
        [SerializeField] private FourthStageCablePoint _firstCablePoint;
        [SerializeField] private FourthStageCablePoint _secondCablePoint;

        public FourthStageCablePoint FirstCablePoint => _firstCablePoint;
        public FourthStageCablePoint SecondCablePoint => _secondCablePoint;

        public void SetCablePair(FourthStageCablePoint firstCablePoint, FourthStageCablePoint secondCablePoint)
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