using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class FourthStagePlomb : MonoBehaviour
    {
        [SerializeField] private GameObject[] _lidGameObjects;
        [SerializeField] private MeshRenderer[] _lidMeshRenderers;
        [Inject] private FourthStageModel _model;
        [Inject] private FourthStageExamSystem _system;

        public bool IsSetupedPlomb { get; private set; }
        public bool IsWrongSetuped { get; private set; }    

        public void SetupPlomb()
        {
            if (_model.IsExitedFromTP)
            {
                _system.SetRightExitFromTP(false);
            }

            //if(!FindObjectOfType<PlakatService>().IsSetupedPlakat)
            //    IsWrongSetuped = true;

            if (_model.IsExitedFromTP && FindObjectOfType<PlakatService>().IsSetupedPlakat)
            {
                Debug.Log("IsSetupedPlomb");
                IsSetupedPlomb = true;
            }
            else if (!_model.IsExitedFromTP)
            {
                Debug.Log("IsSetupedPlomb");
                IsSetupedPlomb = true;
            }

            foreach (var gameObject in _lidGameObjects)
            {
                gameObject.SetActive(true);
            }
            foreach(var item in _lidMeshRenderers)
            {
                item.enabled = true;
            }
            gameObject.SetActive(false);
        }
    }
}