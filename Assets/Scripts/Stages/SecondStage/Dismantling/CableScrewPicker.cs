using Assets.Scripts.Stages.SecondStage.Dismantling.Install_screw;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Dismantling
{
    public class CableScrewPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _counterLid;
        [SerializeField] private LayerMask _ikkLayer;
        [SerializeField] private LayerMask _cableLayer;
        [SerializeField] private LayerMask _screwLayer;

        public bool TryPickScrew(out Screw screw)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _screwLayer))
            {
                if (hit.transform != null && hit.transform.TryGetComponent(out screw))
                {
                    return true;
                }
            }
            screw = null;
            return false;
        }

        public bool TryPickCable(out Cable cable)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _cableLayer))
            {
                if (hit.transform != null && hit.transform.TryGetComponent(out cable))
                {
                    return true;
                }
            }
            cable = null;
            return false;
        }

        public bool TryPickIKK()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _ikkLayer))
            {
                if (hit.transform != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool TryPickCounterLid()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _counterLid))
            {
                if (hit.transform != null)
                {
                    return true;
                }
            }
            return false;
        }
    }

}