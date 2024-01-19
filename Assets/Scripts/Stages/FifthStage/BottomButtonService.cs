using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FifthStage
{
    public class BottomButtonService : MonoBehaviour
    {
        [SerializeField] private Button _iconButton;
        [SerializeField] private GameObject _panel;
        private void Awake()
        {
            _iconButton.onClick.AddListener(Collapse);
        }
        private void Collapse()
        {
            if (_panel.activeSelf)
            {
                _panel.SetActive(false);
            }
            else
            {
                _panel.SetActive(true); 
            }
        }
    }
}