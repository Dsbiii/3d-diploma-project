using Assets.Scripts.Stages.SecondStage;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Instruments
{
    public class CapsService : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        private SecondStageModel _secondStageModel;

        public void Init(SecondStageModel secondStageModel)
        {
            _secondStageModel = secondStageModel;
        }

        public void TrySetupCap()
        {
            
            if(Input.GetMouseButtonDown(0) && _secondStageModel.IsTakedCaps)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
                {
                    if (hit.transform.TryGetComponent(out CapPoint capPoint))
                    {
                        capPoint.SetupCap();
                    }
                }
            }
        }
    }
}