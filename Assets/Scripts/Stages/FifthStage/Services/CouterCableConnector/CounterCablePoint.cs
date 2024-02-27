using Assets.Scripts.Stages.FourthStage;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector
{
    public class CounterCablePoint : MonoBehaviour
    {
        [SerializeField] private GameObject _objectPoint;
        [SerializeField] private FirstComputerPanel _firstComputerPanel;

        [Inject] private FifthStageExam _fifthStageExam;

        public bool IsIndicated { get; private set; }


        public void SetupPoint()
        {
            if(!_firstComputerPanel.IsEnteredInCoumputer)
                _fifthStageExam.ConnectedCounterToPC = true;

            IsIndicated = true;
            _objectPoint.SetActive(true);
            //gameObject.SetActive(false);
        }
    }
}