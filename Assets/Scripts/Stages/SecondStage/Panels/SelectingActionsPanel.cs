using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SecondStage.Panels
{
    public class SelectingActionsPanel : Panel
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _complete;
        private SecondStagePanel _secondStagePanel;

        private void Awake()
        {
            if(_complete != null)
                _complete.onClick.AddListener(Complete);
        }

        public void Init(SecondStagePanel secondStagePanel)
        {
            _secondStagePanel = secondStagePanel;
        }

        public void Complete()
        {
            Close();
            _secondStagePanel.Open();
        }

        public override void Close()
        {
            _panel.SetActive(false);
        }
    }
}