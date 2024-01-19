using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Dismantling
{
    public class PU : MonoBehaviour
    {
        [SerializeField] private GameObject _pu;

        public void DismantlingPU()
        {
            _pu.SetActive(false);
        }
    }
}