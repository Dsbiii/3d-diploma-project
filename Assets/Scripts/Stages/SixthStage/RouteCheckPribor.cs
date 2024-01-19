using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class RouteCheckPribor : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _type;
        [SerializeField] private TMP_Text _equipment;
        public bool IsCorrect;
        public void Check()
        {
            Debug.Log("Robit");//2-2
            if (_name.text == "СЭТ-4ТМ.03, \r\n№0112055629 " && _type.text == "Маршрут через SM160" && _equipment.text == "Подключение к SM160, №01994 (включен)")
            {
                Debug.Log("Robit3");
                IsCorrect = true;
            }
        }
    }
}