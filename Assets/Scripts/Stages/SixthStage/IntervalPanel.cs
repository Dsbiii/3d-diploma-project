using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class IntervalPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _date1;
        [SerializeField] private TMP_Text _date2;
        [SerializeField] private TMP_Text _dateResult;

        public void Sumbit()
        {
            _dateResult.text = $"Начиная с {_date1.text}, период {_date2.text}";
        }

    }
}