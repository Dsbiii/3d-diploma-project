using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class OffPanelsSettings : MonoBehaviour
    {
        [SerializeField] private GameObject[] _panels;


        public void OffPanels()
        {
            foreach (var panel in _panels)
            {
                panel.SetActive(false);
            }
        }
    }
}