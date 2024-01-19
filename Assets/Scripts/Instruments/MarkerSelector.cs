using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Instruments
{
    public class MarkerSelector : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;

        public void TrySetupMarker()
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
                {
                    if (hit.transform.TryGetComponent(out MarkerPoint capPoint))
                    {
                        capPoint.SetupCap();
                    }
                }
            }
        }
    }
}