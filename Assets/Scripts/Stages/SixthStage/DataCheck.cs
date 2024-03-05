using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class DataCheck : MonoBehaviour
    {
        [SerializeField] private TMP_Text _parametr;
        [SerializeField] private TMP_Text _interval;
        [SerializeField] private ObjectService _service;
        public int[] Report = new int[3] {0,0,0}; 
        public int Points()
        {
            int point = 0;
            if (_service.SelectedName != null)
            {
                Debug.Log("check");
                if (_service.SelectedName == "СЭТ-4ТМ.03М")
                {
                    Report[0] = 1;
                    Debug.Log("check2");
                    point += 2;
                }
            }
            if (_parametr.text == "   А+ профиль 1 час")
            {
                Report[2] = 1;
                point += 2;
            }
            if (_interval.text != "")
            {
                Report[1] = 1;
                point += 2;
            }
            return point;
        }
    }
}