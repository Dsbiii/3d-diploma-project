using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.ThirdStage.CablesSystem
{
    public class CablePanel : MonoBehaviour
    {
        [SerializeField] private ThirdStageModel _thirdStageModel;

        [SerializeField] private CableConnector _cableConnector;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _setupButton;
        [SerializeField] private Button _putAwayButton;


        private void Awake()
        {
            _setupButton.onClick.AddListener(Setup);
            _putAwayButton.onClick.AddListener(PutAway);
        }

        public void Setup()
        {
            _cableConnector.SetupCables();
            _thirdStageModel.SetupCables();
            Close();
        }

        public void PutAway()
        {
            _cableConnector.PutAwayCables();
            Close();
        }

        public void Open()
        {
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }

    }
}