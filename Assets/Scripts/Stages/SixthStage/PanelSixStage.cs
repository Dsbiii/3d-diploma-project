using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class PanelSixStage : MonoBehaviour
    {
        [SerializeField] private GameObject[] _topPanels;
        [SerializeField] private GameObject[] _onPanels;
        [SerializeField] private GameObject[] _offPanels;

        public virtual void Open()
        {
            foreach(var panel in _topPanels)
            {
                panel.SetActive(true);
            }
            foreach(var item in _onPanels)
            {
                item.SetActive(true);
            }
        }

        public virtual void Close()
        {
            foreach(var item in _offPanels)
            {
                item.SetActive(false);
            }
        }

    }
}