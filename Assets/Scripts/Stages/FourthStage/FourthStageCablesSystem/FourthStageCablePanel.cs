using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage.CablesSystem
{
    public class FourthStageCablePanel : MonoBehaviour
    {
        [SerializeField] private FourthStageCableConnector _cableConnector;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _setupButton;
        [SerializeField] private Button _putAwayButton;

        [Inject] private FourthStageModel _model;
        [Inject] private FourthStageExamSystem _system;
        public bool IsSetuped { get; private set; }
        private void Awake()
        {
            _setupButton.onClick.AddListener(Setup);
            _putAwayButton.onClick.AddListener(PutAway);
        }

        public void Setup()
        {
            if (_model.IsExitedFromTP)
            {
                _system.SetRightExitFromTP(false);
            }
            IsSetuped = true;
            _cableConnector.SetupCables();
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