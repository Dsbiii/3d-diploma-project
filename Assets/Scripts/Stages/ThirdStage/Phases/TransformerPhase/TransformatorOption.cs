using Assets.Scripts.Stages.ThirdStage.Objects.Counter;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.ThirdStage.Phases.TransformerPhase
{
    public class TransformatorOption : MonoBehaviour , IPointerClickHandler
    {
        public event System.Action<TransformatorOption> OnClicked;
        [SerializeField] private bool _isRight;
        [SerializeField] private Transformer _transformer;

        public Transformer Transformer => _transformer;
        public bool IsRight => _isRight;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this);
        }
    }
}