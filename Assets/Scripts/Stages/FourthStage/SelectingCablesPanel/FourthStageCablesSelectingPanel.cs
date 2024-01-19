using Assets.Scripts.Stages.ThirdStage.CablesSystem;
using Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase;
using Assets.Scripts.Stages.ThirdStage;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;
using Assets.Scripts.Stages.FourthStage.CablesSystem;
using UnityEngine.Rendering.VirtualTexturing;

namespace Assets.Scripts.Stages.FourthStage.SelectingCablesPanel
{
    [System.Serializable]
    public class FourthStageCablePanelSettings
    {
        [SerializeField] private FourthStageCableOption[] _cableOptions;
        [SerializeField] private GameObject _panel;
        [SerializeField] private bool _isLastPanel;
        [SerializeField] private int _rightCount;

        public GameObject Panel => _panel;
        public bool IsLastPanel => _isLastPanel;
        public IReadOnlyCollection<FourthStageCableOption> CableOptions => _cableOptions;
        public bool IsRight => _cableOptions.Where(item => item.IsRight && item.IsSelect).ToArray().Length == _rightCount;

    }

    public class FourthStageCablesSelectingPanel : MonoBehaviour
    {
        [SerializeField] private FourthStageCablePanelSettings[] _cablePanelSettings;
        [SerializeField] private Button[] _nextButtons;

        [Inject] private FourthStageController _fourthStageController;
        [Inject] private FourthStageCableConnector _fourthStageCableConnector;
        [Inject] private FourthStageModel _model;
        [Inject] private FourthStageExamSystem _system;

        private int _currentPanelId = 0;

        public bool CheckRightConnection
        {
            get
            {
                int count = 0;
                foreach(var item in _cablePanelSettings)
                {
                    if(item.IsRight)
                    {
                        count++;
                    }
                }
                if (count >= _cablePanelSettings.Length)
                    return true;
                return false;
            }
        }

        private void Awake()
        {
            foreach (var item in _cablePanelSettings)
            {
                foreach (var option in item.CableOptions)
                    option.OnClicked += SelectItems;

                int rnd = Random.Range(3, 6);
                for (int i = 0; i < rnd; i++)
                {

                    foreach (var option in item.CableOptions)
                    {
                        if (Random.Range(0, 2) == 0)
                        {
                            option.transform.SetAsFirstSibling();
                        }
                        else
                        {
                            option.transform.SetAsLastSibling();
                        }
                    }
                }
            }
            foreach (var item in _nextButtons)
            {
                item.onClick.AddListener(NextPanel);
            }
        }

        private void NextPanel()
        {
            if (_cablePanelSettings[_currentPanelId].IsLastPanel)
            {
                if (_cablePanelSettings.Where(item => item.IsRight).ToArray().Length == _cablePanelSettings.Length)
                {
                    Debug.Log("Правильно выбраны провода");
                }
                _fourthStageController.DisplayCablesConnectPoints();
                _fourthStageCableConnector.Active();
                Close();
            }
            else
            {
                _cablePanelSettings[_currentPanelId].Panel.SetActive(false);
                _currentPanelId++;
                _cablePanelSettings[_currentPanelId].Panel.SetActive(true);
            }
        }

        public void SelectItems(FourthStageCableOption cableOption)
        {

        }

        public void Open()
        {
            if (_model.IsExitedFromTP)
            {
                _system.SetRightExitFromTP(false);
            }
            _cablePanelSettings[_currentPanelId].Panel.SetActive(true);
        }

        public void Close()
        {
            _cablePanelSettings[_currentPanelId].Panel.SetActive(false);
        }

    }
}
