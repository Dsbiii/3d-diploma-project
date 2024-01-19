using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class DeviceCreatePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _serialNumber;
        [SerializeField] private TMP_Text _setupPlace;

        [SerializeField] private TMP_Text _serialValue;
        [SerializeField] private TMP_Text _setupValue;

        public void SetupValues()
        {
            _serialNumber.text = _serialValue.text;
            _setupPlace.text = _setupValue.text;
        }
    }
}