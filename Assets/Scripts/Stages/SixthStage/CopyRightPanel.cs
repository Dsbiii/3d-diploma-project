using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class CopyRightPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _textFrom;
        [SerializeField] private TMP_Text[] _textTo;
        public bool IsCorrect;

        public void CopyRight()
        {
            for (int i = 0; i < _textFrom.Length; i++)
            {
                if (_textFrom[i].text != "Выбрать")
                    _textTo[i].text = _textFrom[i].text;
            }
            Check();
        }
        private void Check()
        {
            if (CheckDate(_textFrom[1].text) && _textFrom[0].text == "СЭТ-4ТМ 0.3М")
            {
                Debug.Log("Robit");//2-3
                IsCorrect = true;
            }
        }
        private bool CheckDate(string date)
        {
            string datestring = date.Remove(date.Length - 1);
            DateTime targetDate = DateTime.ParseExact(datestring, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            // Получение текущей даты
            DateTime currentDate = DateTime.Now;

            // Сравнение дат
            if (targetDate.Date == currentDate.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}