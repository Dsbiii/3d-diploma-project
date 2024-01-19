using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FifthStage
{
    public class OnOffButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject[] _panels;

        private void OnEnable()
        {
            _button.onClick.AddListener(Click);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Click);
        }

        public void Click()
        {
            foreach(var item in _panels)
            {
                if(item != _panel)
                    item.SetActive(false);
            }
            _panel.SetActive(!_panel.activeSelf);
        }
    }
}