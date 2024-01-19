using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage
{
    public class DateAndClocksPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dateTimeText;
        private void Update()
        {
            UpdateFormattedDateTime();
        }

        private void UpdateFormattedDateTime()
        {
            DateTime currentDateTime = DateTime.Now;

            string formattedDateTime = currentDateTime.ToString("HH:mm dd.MM.yyyy");

            _dateTimeText.text = formattedDateTime;
        }
    }
}