using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class DataCheck : MonoBehaviour
    {
        [SerializeField] private TMP_Text _parametr;
        [SerializeField] private TMP_Text _interval;
        public int Points()
        {
            int point = 2;
            if (_parametr.text == "   А+ профиль 1 час")
                point += 2;
            if (_interval.text != "")
                point += 2;
            return point;
        }
    }
}