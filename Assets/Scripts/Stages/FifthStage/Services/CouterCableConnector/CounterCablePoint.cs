﻿using Assets.Scripts.Stages.FifthStage.Panels;
using Assets.Scripts.Stages.FourthStage;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector
{
    public class CounterCablePoint : MonoBehaviour
    {
        [SerializeField] private SATPanelObject _sATPanelObject;
        [SerializeField] private GameObject _objectPoint;
        [SerializeField] private SMPanel _smPanel;

        [Inject] private FifthStageExam _fifthStageExam;

        public bool IsIndicated { get; private set; }


        public void SetupPoint()
        {
            //if(!_smPanel.IsOpened && !_sATPanelObject.IsOpenedSatPanel)
            //    _fifthStageExam.ConnectedCounterToPC = true;

            if (!_sATPanelObject.IsOpenedSatPanel)
                _fifthStageExam.ConnectedCounterToPC = true;

            IsIndicated = true;
            _objectPoint.SetActive(true);
            //gameObject.SetActive(false);
        }
    }
}