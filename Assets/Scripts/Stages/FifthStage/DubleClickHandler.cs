using Assets.Scripts.Stages.FifthStage.Panels;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage
{
    public class DubleClickHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SATPanel _sATPanel;
        [SerializeField] private GameObject _appPanel;
        [SerializeField] private GameObject _downIcon;
        [SerializeField] private bool _isDemo;
        [Inject] private GameMode _gameMode;
        private float doubleClickTimeThreshold = 0.3f;
        private float lastClickTime;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_gameMode.IsDemo && _isDemo) 
            {
                return;
            }
            if (Time.time - lastClickTime < doubleClickTimeThreshold)
            {
                lastClickTime = 0f;
                _appPanel.SetActive(true);
                _downIcon.SetActive(true);
            }
            else
            {
                lastClickTime = Time.time;
            }
        }
    }
}