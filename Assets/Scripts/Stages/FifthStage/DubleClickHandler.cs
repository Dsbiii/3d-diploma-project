using Assets.Scripts.Stages.FifthStage.Panels;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.FifthStage
{
    public class DubleClickHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SATPanel _sATPanel;
        [SerializeField] private GameObject _appPanel;
        [SerializeField] private GameObject _downIcon;
        private float doubleClickTimeThreshold = 0.3f;
        private float lastClickTime;

        public void OnPointerClick(PointerEventData eventData)
        {

            if (Time.time - lastClickTime < doubleClickTimeThreshold)
            {
                lastClickTime = 0f;
                _appPanel.SetActive(true);
                _downIcon.SetActive(true);
                if(_sATPanel != null)
                {
                    _sATPanel.CheckRight();
                }
            }
            else
            {
                lastClickTime = Time.time;
            }
        }
    }
}