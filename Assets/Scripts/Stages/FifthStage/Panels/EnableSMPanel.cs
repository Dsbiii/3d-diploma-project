using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class EnableSMPanel : MonoBehaviour
    {
        [SerializeField] private GameObject[] _panels;

        private void OnEnable()
        {
            foreach (var panel in _panels)
            {
                panel.SetActive(false);
            }
        }
    }
}