using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class TogglesPanel : MonoBehaviour
    {
        [SerializeField] private SMPanel _sMPanel;
        [SerializeField] private List<Toggle> _toggles;
        [SerializeField] private Image _infoPanel;

        public bool IsRight { get; private set; }

        //private void OnEnable()
        //{
        //    _toggles = GetComponentsInChildren<Toggle>().Where(item => item.enabled).ToList();
        //    foreach(var item in _toggles)
        //    {
        //        item.isOn = false;
        //    }
        //}

        private void OnEnable()
        {
            _infoPanel.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _infoPanel.gameObject.SetActive(false);
            _sMPanel.CloseOffPanels();
        }

        private void Update()
        {
            int count = 0;

            foreach(var item in _toggles)
            {
                if(item.isOn)
                {
                    count++;
                }
            }
            Debug.Log(count);
            if(count + 9 <= 10)
            {
                IsRight = false;
                _sMPanel.OpenOffPanels();
                _infoPanel.color = Color.gray;
            }
            else if (count + 9 == 24)
            {
                IsRight = true;
                _sMPanel.CloseOffPanels();
                _infoPanel.color = Color.gray;
            }
            else if(count + 9 > 24)
            {
                IsRight = false;
                _sMPanel.OpenOffPanels();
                _infoPanel.color = Color.red;
            }
        }

    }
}