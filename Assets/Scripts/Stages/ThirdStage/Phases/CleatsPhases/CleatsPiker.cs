using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases.CleatsPhases
{
    public class CleatsPiker : MonoBehaviour
    {
        [SerializeField] private LayerMask _cleatLayer;

        public void TryPeackCleat()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _cleatLayer))
            {
                if (hit.transform != null)
                {
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }
}