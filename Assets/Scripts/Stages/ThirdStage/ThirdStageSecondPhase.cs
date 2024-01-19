using Assets.Scripts.Stages.SecondStage;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage
{
    public class ThirdStageSecondPhase : MonoBehaviour
    {
        [SerializeField] private SecondStageController _secondStageController;
        [SerializeField] private SecondStageState _secondStageState;
        [SerializeField] private GameObject[] _plakats;
        [SerializeField] private BreakingInstumentSevice _breakingInstumentSevice;

        public void PreparePhase()
        {
            _secondStageState.EnterInInspection();
            _breakingInstumentSevice.Enable();
            foreach (var item in _plakats)
                item.SetActive(false);
        }

        public void PrepareThirdPhase()
        {
            _secondStageController.EnterInThirdPhaseThirdStage();
        }
    }
}