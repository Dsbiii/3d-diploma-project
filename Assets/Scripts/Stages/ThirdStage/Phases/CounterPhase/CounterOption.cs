using Assets.Scripts.Stages.ThirdStage.Objects.Counter;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.ThirdStage.Phases.CounterPhase
{
    public class CounterOption : MonoBehaviour, IPointerClickHandler
    {
        public event System.Action<CounterOption> OnClicked;
        [SerializeField] private bool _isRight;
        [SerializeField] private Counter _counter;

        public Counter Counter => _counter;
        public bool IsRight => _isRight;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this);
        }
    }
}