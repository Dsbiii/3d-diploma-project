using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage
{
    public class Gercon : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gercons;


        [SerializeField] private Setting _setting;


        void Start()
        {
            //var gercon = _gercons[Random.Range(0, _gercons.Length)];
            //gercon.name = "GerconOn";
            //gercon.SetActive(true);
        }


    }
}