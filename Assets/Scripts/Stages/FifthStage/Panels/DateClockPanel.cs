using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class DateClockPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dateText;
        [SerializeField] private TMP_Text _timeText;


        private void Update()
        {
            UpdateFormattedDateTime();
        }

        private void UpdateFormattedDateTime()
        {
            DateTime currentDateTime = DateTime.Now;

            string formattedTime = currentDateTime.ToString("HH:mm:ss.fff");
            string formattedDate = currentDateTime.ToString("dd.MM.yyyy");

            _dateText.text = formattedDate;
            _timeText.text = formattedTime;
        }
    }
}