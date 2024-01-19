using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class IncreaseDeacreaseValue : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;
        private int number = 0;

        // Функция для увеличения числа
        public void IncreaseNumber()
        {
            number++; // Увеличиваем число на 1
            _value.text = number.ToString();
        }

        // Функция для уменьшения числа
        public void DecreaseNumber()
        {
            if (number > 0)
            {
                number--;
            }
            _value.text = number.ToString();
        }
    }
}