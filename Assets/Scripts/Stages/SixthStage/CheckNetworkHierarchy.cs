using Assets.Scripts.Stages.SixthStage.Report;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class CheckNetworkHierarchy : MonoBehaviour
    {
        [SerializeField] private GameObject _setting;
        [SerializeField] private TMP_Text _interval;
        [SerializeField] private GameObject _panel;
        private bool _settingChoiced;
        private bool _intervalChoiced;
        
        private void Update()
        {
            if (_setting.activeSelf)
            {
                _settingChoiced = true;
            }
            else
            {
                _settingChoiced = false;
            }
            if(_interval.text.Length > 0)
            {
                _intervalChoiced = true;
            }
            else
            {
                _intervalChoiced = false;
            }
        }
        public void ClickUpdate()
        {
            if( _settingChoiced && _intervalChoiced)
            {
                _panel.SetActive(true);
            }
        }
    }
}