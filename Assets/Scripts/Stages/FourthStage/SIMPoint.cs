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
        [Inject] private GameMode _gameMode;

        public bool IsIndicated { get; private set; }

        public void TryDisplayIndicator()
        {
            if(IsIndicated)
                _indicator.SetActive(true);
        }
        public void OnEnable()
        {
            if (_gameMode.IsDemo)
            {
                SetupPoint();
            } 
        }
        public void SetupPoint()
        {
            if (_model.IsExitedFromTP)
            {
                _system.SetCriticalError();
                _system.SetRightExitFromTP(false);
            }
            else
            {
                IsIndicated = true;
            }
            _objectAntena.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}