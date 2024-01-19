using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class ComputerPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _computerPanel;


        public void Open()
        {
            _computerPanel.SetActive(true);
        }

        public void Close()
        {
            _computerPanel.SetActive(false);
        }

    }
}