using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FifthStage
{
    public class WindowButtonService : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _minimizeButton;
        [SerializeField] private Button _expandButton;
        [SerializeField] private GameObject _appPanel;
        [SerializeField] private GameObject _downPanel;
        private bool _expanded = false;
        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
            _minimizeButton.onClick.AddListener(Minimize);
        }
        public void Close()
        {
            _appPanel.SetActive(false);
            _downPanel.SetActive(false);
        }
        private void Expand()
        {
            if (!_expanded)
            {
                SetWindowSize(1, 1);
                //SetWindowPosition(20);
                _expanded = true;
            }
            else
            {
                SetWindowSize(0.7f, 0.7f);
                //SetWindowPosition(-20);
                _expanded = false;
            }
        }
        private void Minimize()
        {
            _appPanel.SetActive(false);
        }
        private void SetWindowSize(float targetWidht, float targetHeight)
        {
            //_appPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(targetWidht, targetHeight);
            _appPanel.transform.localScale = new Vector2(targetWidht, targetHeight);
        }
        private void SetWindowPosition(float PossitionY)
        {
            _appPanel.transform.position = new Vector2(_appPanel.transform.position.x, _appPanel.transform.position.y + PossitionY);
        }

    }
}