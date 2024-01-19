using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase
{
    public class CablePoint : MonoBehaviour
    {
        [SerializeField] private CablePointType _cablePointType;
        [SerializeField] private CableComponent _cableComponent;
        public bool IsConnecting { get; private set; }

        public CablePointType CablePointType => _cablePointType;

        public void ConnectCables(Transform point)
        {
            _cableComponent.enabled = true;
            _cableComponent.ConnectCable(point);
            IsConnecting = true;
        }

        public void PullOut()
        {
            IsConnecting = false;
            _cableComponent.PullOutCable();
        }

        public void Connect()
        {
            IsConnecting = true;
        }

    }
}