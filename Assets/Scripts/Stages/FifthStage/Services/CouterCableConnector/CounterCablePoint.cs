using Assets.Scripts.Stages.FourthStage;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector
{
    public class CounterCablePoint : MonoBehaviour
    {
        [SerializeField] private GameObject _objectPoint;

        [Inject] private FifthStageExam _fifthStageExam;

        public bool IsIndicated { get; private set; }


        public void SetupPoint()
        {
            _fifthStageExam.ConnectedCounterToPC = true;
            IsIndicated = true;
            _objectPoint.SetActive(true);
            //gameObject.SetActive(false);
        }
    }
}