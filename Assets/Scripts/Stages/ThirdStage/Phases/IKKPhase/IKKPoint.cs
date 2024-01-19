using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases.IKKPhase
{
    public class IKKPoint : MonoBehaviour
    {
        public bool IsRight;

        [SerializeField] private GameObject _pointObjects;

        public bool IsPlanted { get; private set; }

        public void Plant()
        {
            IsPlanted = true;
        }

        public void TryDisplayPoint()
        {
            if (IsPlanted)
                return;
            _pointObjects.SetActive(true);
        }

        public void OffPoint()
        {
            _pointObjects.SetActive(false);
        }
    }
}