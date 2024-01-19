using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class SIMPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _indicator;
        [SerializeField] private GameObject _objectAntena;
        [Inject] private FourthStageModel _model;
        [Inject] private FourthStageExamSystem _system;

        public bool IsIndicated { get; private set; }

        public void TryDisplayIndicator()
        {
            if(IsIndicated)
                _indicator.SetActive(true);
        }

        public void SetupPoint()
        {
            if (_model.IsExitedFromTP)
            {
                _system.SetRightExitFromTP(false);
            }
            IsIndicated = true;
            _objectAntena.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}