using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage.Tools
{
    public class OnOffToPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _targetPanel;
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject _panelOff;


        private void Update()
        {
            _panel.SetActive(_targetPanel.activeSelf);
            _panelOff.SetActive(!_targetPanel.activeSelf);
        }

    }
}