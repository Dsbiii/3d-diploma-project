using Assets.Scripts.Instruments;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField] private PlombPoint _ceViewPlombPoint;

        [SerializeField] private BoxCollider _counterLidBoxColider;
        [SerializeField] private MeshRenderer[] _counterLid;
        private SecondStageModel _secondStageModel;

        public void Init(SecondStageModel secondStageModel)
        {
            _secondStageModel = secondStageModel;
        }

        public void OpenLid()
        {
            if (_secondStageModel.IsTakedScrewdriver)
            {
                _ceViewPlombPoint.Close();
                _ceViewPlombPoint.OffColliders();

                _counterLidBoxColider.enabled = false;
                foreach (var item in _counterLid)
                    item.enabled = false;
            }
        }

        public void EnableColladers()
        {
            _counterLidBoxColider.enabled = true;
        }

        public void OpenLidWithoutScrewdriver()
        {
            _ceViewPlombPoint.Close();
            _ceViewPlombPoint.OffColliders();

            _counterLidBoxColider.enabled = false;
            foreach (var item in _counterLid)
                item.enabled = false;
        }

        public void CloseLidWithoutScrewdriver()
        {
            _counterLidBoxColider.enabled = true;
            foreach (var item in _counterLid)
                item.enabled = true;
        }

        public void CloseLid()
        {
            if (_secondStageModel.IsTakedScrewdriver)
            {
                _counterLidBoxColider.enabled = true;
                foreach (var item in _counterLid)
                    item.enabled = true;
            }
        }
    }
}