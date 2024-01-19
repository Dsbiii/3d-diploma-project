using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class ToggleSelector : MonoBehaviour
    {
        public Toggle[] toggles; // массив тоглов

        void Start()
        {
            foreach (Toggle toggle in toggles)
            {
                toggle.onValueChanged.AddListener(delegate { OnToggleValueChanged(toggle); }); // добавляем слушателя события изменения состояния тогла
            }
        }

        // Функция, вызываемая при изменении состояния тогла
        void OnToggleValueChanged(Toggle selectedToggle)
        {
            if (selectedToggle.isOn)
            {
                foreach (Toggle toggle in toggles)
                {
                    if (toggle != selectedToggle)
                    {
                        toggle.isOn = false; // отключаем все тоглы, кроме выбранного
                    }
                }
            }
        }
    }
}