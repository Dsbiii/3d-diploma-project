using Assets.Scripts.Instruments;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Dismantling
{
    public class IKK : MonoBehaviour
    {
        [SerializeField] private PlombPoint _ikkPlomb;
        [SerializeField] private BoxCollider _lidColider;
        [SerializeField] private MeshRenderer _lid;
        [SerializeField] private MeshRenderer[] _plomb;
        private SecondStageModel _secondStageModel;

        public void Init(SecondStageModel secondStageModel)
        {
            _secondStageModel = secondStageModel;
        }

        public void Close()
        {
            if (_secondStageModel.IsTakedScrewdriver)
            {
                _lidColider.enabled = true;
                _lid.enabled = true;
                foreach (var item in _plomb)
                    item.enabled = true;
            }
        }

        public void EnableColladers()
        {
            _lidColider.enabled = true;
        }

        public void CloseWithOutScrewDriver()
        {

            _lidColider.enabled = true;
            _lid.enabled = true;
            foreach (var item in _plomb)
                item.enabled = true;
        }

        public void OpenWithOutScrewDriver()
        {
            _ikkPlomb.Close();
            _ikkPlomb.OffColliders();
            _lidColider.enabled = false;
            _lid.enabled = false;
            foreach (var item in _plomb)
                item.enabled = false;
        }

        public void Open()
        {
            if (_secondStageModel.IsTakedScrewdriver)
            {
                _ikkPlomb.Close();
                _ikkPlomb.OffColliders();

                _lidColider.enabled = false;
                _lid.enabled = false;
                foreach (var item in _plomb)
                    item.enabled = false;
            }
        }
    }
}