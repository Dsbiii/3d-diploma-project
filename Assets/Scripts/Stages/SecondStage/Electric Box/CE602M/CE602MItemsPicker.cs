using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M
{
    public class CE602MItemsPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _magnitePointlayerMask;

        public bool TryPickMagnitePoint(out MagnitePoint magnitePoint)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity , _magnitePointlayerMask))
            {
                if (hit.transform.TryGetComponent(out magnitePoint))
                {
                    return true;
                }
            }
            magnitePoint = null;
            return false;
        }
    }
}