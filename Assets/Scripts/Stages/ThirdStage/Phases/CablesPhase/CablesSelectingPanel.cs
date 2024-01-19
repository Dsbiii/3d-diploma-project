using Assets.Scripts.Stages.ThirdStage.CablesSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase
{
    [System.Serializable]
    public class CablePanelSettings
    {
        [SerializeField] private CableOption[] _cableOptions;
        [SerializeField] private GameObject _panel;
        [SerializeField] private bool _isLastPanel;
        [SerializeField] private int _rightCount;

        public GameObject Panel => _panel;
        public bool IsLastPanel => _isLastPanel;
        public IReadOnlyCollection<CableOption> CableOptions => _cableOptions;
        public bool IsRight => _cableOptions.Where(item => item.IsRight && item.IsSelect).ToArray().Length == _rightCount;

    }

    public class CablesSelectingPanel : MonoBehaviour
    {
        [SerializeField] private CablePanelSettings[] _cablePanelSettings;
        [SerializeField] private Button[] _nextButtons;
        [SerializeField] private CableConnector _cableConnector;
        private CablesPhase _cablesPhase;
        private ThirdStageModel _thirdStageModel;

        private int _currentPanelId = 0;

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
            _thirdStageModel = FindObjectOfType<ThirdStageModel>();
            foreach(var item in _nextButtons)
            {
                item.onClick.AddListener(NextPanel);
            }
        }

        public void Init(CablesPhase cablesPhase)
        {
            _cablesPhase = cablesPhase;
        }

        private void NextPanel()
        {
            if (_cablePanelSettings[_currentPanelId].IsLastPanel)
            {
                if (_cablePanelSettings.Where(item => item.IsRight).ToArray().Length == _cablePanelSettings.Length)
                    _thirdStageModel.IsRightSelectedCables = true;

                _cableConnector.Active();
                Close();
            }
            else
            {
                _cablePanelSettings[_currentPanelId].Panel.SetActive(false);
                _currentPanelId++;
                _cablePanelSettings[_currentPanelId].Panel.SetActive(true);
            }
        }

        public void SelectItems(CableOption cableOption)
        {
            
        }

        public void Open()
        {
            _cablePanelSettings[_currentPanelId].Panel.SetActive(true);
        }

        public void Close()
        {
            _cablePanelSettings[_currentPanelId].Panel.SetActive(false);
        }

    }
}