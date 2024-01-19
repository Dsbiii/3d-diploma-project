using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M
{
    public class CablePort : MonoBehaviour
    {
        [SerializeField] private CableComponent _cableComponent;
        [SerializeField] private MagnitePointsTypes _magnitePointsTypes;
        [SerializeField] private MagnitePointsTypes _magnitePointsColor;

        public MagnitePointsTypes MagnitePointsColor => _magnitePointsColor;
        public MagnitePointsTypes MagnitePointsTypes => _magnitePointsTypes;
        public bool IsEmpty => !_cableComponent.IsConnectingCable;

        public void PullOutCable()
        {
            _cableComponent.PullOutCable();
        }

        public void Connect(Transform point)
        {
            var cablePoint = point.Find("CablePoint");
            if(cablePoint != null)
                _cableComponent.ConnectCable(cablePoint.transform);
            else
                _cableComponent.ConnectCable(point);
        }

    }
}