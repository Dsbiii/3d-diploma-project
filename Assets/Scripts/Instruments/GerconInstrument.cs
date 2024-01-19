using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Instruments
{
    public class GerconInstrument : MonoBehaviour
    {
        [SerializeField] private GameObject _gercon;

        public void Open()
        {
            _gercon.SetActive(true);
        }

        public void Close()
        {
            _gercon.SetActive(false);
        }
    }
}