using Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector;
using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class DateClockPanel : MonoBehaviour
    {
        [SerializeField] private FifthStageModel _stageModel;
        [SerializeField] private CounterCablePoint _counterCablePoint;
        [SerializeField] private TMP_Text _dateText;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private int _lagSeconds;


        private void Update()
        {
            if (!_counterCablePoint.IsIndicated)
                return;
            if (!_stageModel.IsRightConnectedComputer)
                return;
            UpdateFormattedDateTime();
        }

        private void UpdateFormattedDateTime()
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime adjustedTime = currentDateTime.AddSeconds(_lagSeconds);

            string formattedTime = adjustedTime.ToString("HH:mm:ss.fff");
            string formattedDate = currentDateTime.ToString("dd.MM.yyyy");

            _dateText.text = formattedDate;
            _timeText.text = formattedTime;
        }
    }
}