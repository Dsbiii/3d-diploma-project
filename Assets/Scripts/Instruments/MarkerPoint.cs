using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Instruments
{
    public class MarkerPoint : MonoBehaviour
    {
        public void SetupCap()
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}