using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box
{
    public class DoorPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _doorLayerMask;

        public bool TryPickDoor(out Door door)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _doorLayerMask))
            {
                if (hit.transform != null && hit.transform.TryGetComponent(out door))
                {
                    return true;
                }
            }
            door = null;
            return false;
        }
    }
}