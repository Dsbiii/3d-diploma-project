using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases.CleatsPhases
{
    public class CleatsPhases : MonoBehaviour
    {
        private CleatsPiker _cleatsPiker;


        public void Init(CleatsPiker cleatsPiker)
        {
            _cleatsPiker = cleatsPiker;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _cleatsPiker.TryPeackCleat();
            }
        }

    }
}