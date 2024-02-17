using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages
{
    [ExecuteInEditMode]
    public class DatedItemService : MonoBehaviour
    {
        [Header("Максимальный и минимальный разборс в днях")]
        [SerializeField] private DateRange _right;
        [SerializeField] private DateRange _wrong;

        [Header("Текста с датой")]
        [SerializeField] private string _dateText;

        [SerializeField] private TMP_Text[] _dateTextes;
        private DateRange _currentRange;
        private string _currentDateString;

        public string Date { get; private set; }

        [field: SerializeField] public bool IsOverdue { get; private set; }

        //private void OnEnable()
        //{
        //    List<TMP_Text> texts = new List<TMP_Text>();

        //    foreach (Transform child in GetComponentsInChildren<Transform>())
        //    {
        //        if (child.gameObject.TryGetComponent(out TMP_Text tMP_Text))
        //            texts.Add(tMP_Text);
        //    }
        //    _dateTextes = texts.ToArray();
        //}

        public void DisplayDate()
        {
            int Ran = UnityEngine.Random.Range(0, 99);
            if (Ran > 60)
            {
                DisplayWrongDate();
            }
            else
            {
                DiplayRightDate();
            }

        }

        public void Replace()
        {
            if (IsOverdue || _currentDateString ==  DateTime.Today.ToString("dd MM yyyy"))
            {
                IsOverdue = false;
                DiplayRightDate();
            }
        }

        private void DiplayRightDate()
        {
            DisplayDate(_right, false);
        }

        private void DisplayWrongDate()
        {
            DisplayDate(_wrong, true);
        }

        private void DisplayDate(DateRange dateRange, bool isOverdue)
        {
            _currentRange = dateRange;
            StringBuilder sb = new StringBuilder();
            string dateText = "";
            char[] dates = _dateText.ToCharArray();
            for (int i  =0; i < dates.Length; i++)
            {
                if (dates[i] == 'n')
                {
                    int x1 = dateText.Length - 1;
                    dateText = dateText.Remove(x1);
                    sb.AppendLine(dateText);
                    dateText = "";
                }
                else
                {
                    dateText += dates[i];
                }
            }
            _currentDateString = DateTime.Today.AddDays(UnityEngine.Random.Range(dateRange.DaysMinRange, dateRange.DaysMaxRange)).ToString("dd MM yyyy");
            //_currentDateString = DateTime.Today.ToString("dd MM yyyy");
            string Data = sb.ToString() + _currentDateString;
            Date = _currentDateString;
            if (_dateTextes == null)
                return;
            IsOverdue = isOverdue;

            for (int i = 0; i < _dateTextes.Length; i++)
            {
                _dateTextes[i].text = Data;
            }
        }
    }

    [Serializable]
    public struct DateRange
    {
        [SerializeField] private int _daysMinRange;
        [SerializeField] private int _daysMaxRange;

        public int DaysMinRange => _daysMinRange;
        public int DaysMaxRange => _daysMaxRange;
    }
}