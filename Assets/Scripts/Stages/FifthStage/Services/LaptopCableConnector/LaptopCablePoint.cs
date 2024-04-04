using Assets.Scripts.Stages.FifthStage.Panels;
using Assets.Scripts.Stages.FourthStage;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector
{
    public class LaptopCablePoint : MonoBehaviour
    {
        [SerializeField] private SATPanelObject _sATPanelObject;
        [SerializeField] private GameObject _objectPoint;
        [SerializeField] private SMPanel _smPanel;
        [Inject] private FifthStageExam _fifthStageExam;

        public bool IsIndicated { get; private set; }

        public void SetupPoint()
        {
            //if (!_smPanel.IsOpened && !_sATPanelObject.IsOpenedSatPanel)
            //    _fifthStageExam.ConnectedUspdToPC = true;

            if (!_smPanel.IsOpened)
                _fifthStageExam.ConnectedUspdToPC = true;

            IsIndicated = true;
            _objectPoint.SetActive(true);
            //gameObject.SetActive(false);
        }
    }
}