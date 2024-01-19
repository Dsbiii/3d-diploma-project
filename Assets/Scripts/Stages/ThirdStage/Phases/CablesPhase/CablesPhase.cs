using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase
{
    public class CablesPhase : MonoBehaviour
    {
        private bool _isSelectingCables;
        private CablesConnector _cablesConnector;

        public void Init(CablesConnector cablesConnector)
        {
            _cablesConnector = cablesConnector;
        }

        public void SelectedCables()
        {
            _isSelectingCables = true;
        }

        private void Update()
        {
            if (_isSelectingCables)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _cablesConnector.TryConnectCable();
                }
            }
        }

    }
}