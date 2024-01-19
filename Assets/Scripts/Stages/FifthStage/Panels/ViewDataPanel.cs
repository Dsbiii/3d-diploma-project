using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class ViewDataPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;

        public void Open()
        {
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }

    }
}