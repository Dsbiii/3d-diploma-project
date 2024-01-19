using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        public event System.Action<Window> OnOpen;

        public GameObject Panel => _panel;

        public virtual void Open()
        {
            _panel.SetActive(true);
            OnOpen?.Invoke(this);
        }

        public void OpenWithOutEvent()
        {
            _panel.SetActive(true);

        }

        public virtual void Close()
        {
            _panel.SetActive(false);
        }
    }
}