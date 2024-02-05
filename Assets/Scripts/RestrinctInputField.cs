using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class RestrinctInputField : MonoBehaviour
    {
        public InputField inputField;

        private void Start()
        {
            if (inputField != null)
            {
                inputField.onValidateInput += ValidateInput;
            }
        }

        private char ValidateInput(string text, int charIndex, char addedChar)
        {
            // Проверяем, является ли добавленный символ тем, который нужно запретить
            if (addedChar == '-' || !char.IsDigit(addedChar))
            {
                // Если это символ "-", то не разрешаем его ввод
                return '\0'; // '\0' - это символ, который представляет пустой символ (ничего не добавляем)
            }

            // Если символ разрешен, возвращаем его
            return addedChar;
        }
    }
}