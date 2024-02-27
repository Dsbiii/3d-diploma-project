using Assets.Scripts.Stages.FifthStage;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class AntenaPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _objectAntena;
        [Inject] private FourthStageModel _model;
        [Inject] private FourthStageExamSystem _system;

        public bool IsSetupedAntena { get; private set; }

        public void SetupAntena()
        {
            if (_model.IsExitedFromTP)
            {
                _system.SetCriticalError();
                _system.SetRightExitFromTP(false);
            }
            else
            {
                IsSetupedAntena = true;
            }
            _objectAntena.SetActive(true);
            gameObject.SetActive(false);
        }

    }
}