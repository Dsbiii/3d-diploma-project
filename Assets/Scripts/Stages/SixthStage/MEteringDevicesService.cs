using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class MEteringDevicesService : MonoBehaviour
    {
        [SerializeField] private List<MeteringDevicesObject> _objects;
        [SerializeField] private TMP_Text _deviceName;
        [SerializeField] private ChanelForming _routePrefab;
        [SerializeField] private Transform _parentRoute;

        public void Selected(MeteringDevicesObject obj)
        {
            foreach(MeteringDevicesObject obj2 in _objects)
            {
                if (obj2 != obj)
                {
                    obj2.Unselect();
                }
            }
        }
        public void SetDeviceName(string deviceName)
        {
            _deviceName.text = deviceName;
        }
    }
}