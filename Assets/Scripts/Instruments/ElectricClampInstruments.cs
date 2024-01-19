using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Instruments
{
    public class ElectricClampInstruments : MonoBehaviour
    {
        [SerializeField] private GameObject _parmaTablo;
        [SerializeField] private ElectricClamp _electricClamp;
        [SerializeField] private GameObject[] _electicClamps;

        public void Open()
        {
            _parmaTablo.SetActive(true);
            _electricClamp.ResetPosition();
            _electricClamp.gameObject.SetActive(true);
        }

        public void Close()
        {
            _parmaTablo.SetActive(false);
            foreach (var item in _electicClamps)
                item.SetActive(false);
            _electricClamp.ResetPosition();
            _electricClamp.gameObject.SetActive(false);
        }

        public void ResetInstrument()
        {
            foreach (var item in _electicClamps)
                item.SetActive(false);
            _electricClamp.ResetPosition();
        }

    }
}