using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases.CounterPhase
{
    public class Ruler : MonoBehaviour
    {
        [SerializeField] private GameObject _rullerPanel;
        [SerializeField] private TMP_Text _value;
        [SerializeField] private Transform _downerPoint;

        private Transform _counter;

        public void SetCounter(Transform counter)
        {
            _counter = counter;
        }

        public void Open()
        {
            _rullerPanel.SetActive(true);
        }

        public void Close()
        {
            _rullerPanel.SetActive(false);
        }

        public void Update()
        {
            if(_counter != null && _rullerPanel.activeSelf)
                _value.text = (_counter.transform.position.y - _downerPoint.transform.position.y).ToString() + "м";
        }

    }
}