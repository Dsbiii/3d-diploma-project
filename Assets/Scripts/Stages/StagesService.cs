using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.SecondStage;
using Assets.Scripts.Stages.SecondStage.Panels;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stages
{

    public class StagesService : MonoBehaviour
    {
        [SerializeField] private SecondStageState _secondStageState;
        [SerializeField] private FirstStageController _firstStageController;

        [Header("Second stage fields")]
        [SerializeField] private GameObject _necessarilyPanel;
        [SerializeField] private SecondStagePanel _secondStagePanel;
        [SerializeField] private SecondStageController _secondStageController;
        [SerializeField] private ReportSelectObyDeystviy _reportSelectObyDeystviy;


        private void Start()
        {
            if (PlayerPrefs.HasKey("PreLoadStage") && _secondStagePanel != null)
            {
                if(PlayerPrefs.GetInt("PreLoadStage") == 3)
                {
                    EnterInAddmissionStage();
                    _necessarilyPanel.SetActive(false);
                    _secondStageState.EnterInDismantlingState();
                }
                PlayerPrefs.DeleteKey("PreLoadStage");
            }
            else if(_secondStagePanel != null)
            {
                _secondStagePanel.EnterInDosmatlingStage();
            }
        }

        public void EnterInAddmissionStage()
        {
            _reportSelectObyDeystviy.Select();
            _secondStageController.EndInspection(true);
            _secondStageController.EndDismalting(true);
            _secondStagePanel.AddObligatoryItems();
            _secondStagePanel.EnterNextState(false);
        }


        public void SetupAddmissionStageNext()
        {
            PlayerPrefs.SetInt("PreLoadStage", 3);
            _firstStageController.Complite();
        }

    }
}