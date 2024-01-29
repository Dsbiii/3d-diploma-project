using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class Pasport : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _type;
        [SerializeField] private TMP_Dropdown _abonent;
        [SerializeField] private AbonentPanel _abonentPanel;
        private void Awake()
        {
            _abonent.AddOptions(_abonentPanel.GetText());
        }
        public int Points()
        {
            int Point = 0;
            if (_type.value == 2)
                Point++;
            if (_abonent.value > 2)
                Point++;
            return Point;
        }
    }
}