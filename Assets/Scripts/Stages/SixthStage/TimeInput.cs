using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeInput : MonoBehaviour
{
    public TMP_InputField timeInputField;

    private void Start()
    {
        // Добавляем слушателя для события изменения текста в InputField
        timeInputField.onValueChanged.AddListener(OnTimeValueChanged);
    }

    // Функция, вызываемая при изменении текста в InputField
    private void OnTimeValueChanged(string newValue)
    {
        // Проверяем, соответствует ли ввод формату HH:MM:SS
        if (IsInputValid(newValue))
        {
            // Разделяем ввод на часы, минуты и секунды
            string[] timeParts = newValue.Split(':');

            if (timeParts.Length == 3)
            {
                // Преобразуем каждую часть времени в числовое значение
                int hours, minutes, seconds;
                if (int.TryParse(timeParts[0], out hours) &&
                    int.TryParse(timeParts[1], out minutes) &&
                    int.TryParse(timeParts[2], out seconds))
                {
                    // Обновляем значение в нужном формате
                    string formattedTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
                    timeInputField.text = formattedTime;
                }
            }
        }
    }

    // Функция для проверки корректности ввода времени
    private bool IsInputValid(string input)
    {
        // Проверяем, соответствует ли ввод формату HH:MM:SS
        return System.Text.RegularExpressions.Regex.IsMatch(input, @"([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]");
    }
}
