using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage.Structure
{
    public class IntervalDatePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _date1;
        [SerializeField] private TMP_Text _date2;
        [SerializeField] private TMP_Text _dateResult;

        public void SumbitDate()
        {
            _dateResult.text = $"{_date1.text} 00:00:00 - {_date2.text} 00:00:00";
        }

    }
}