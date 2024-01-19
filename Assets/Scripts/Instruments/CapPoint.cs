using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Instruments
{
    public class CapPoint : MonoBehaviour
    {
        public bool IsActiveCap { get; private set; }

        public void SetupCap()
        {
            GetComponent<MeshRenderer>().enabled = true;
            IsActiveCap = true;
        }
    }
}