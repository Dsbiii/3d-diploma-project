using Assets.Scripts.Stages.FourthStage;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector
{
    public class LaptopCablePoint : MonoBehaviour
    {
        [SerializeField] private GameObject _objectPoint;
        [Inject] private FifthStageExam _fifthStageExam;

        public bool IsIndicated { get; private set; } = true;

        public void SetupPoint()
        {
            _fifthStageExam.ConnectedUspdToPC = true;
            IsIndicated = true;
            _objectPoint.SetActive(true);
            //gameObject.SetActive(false);
        }
    }
}