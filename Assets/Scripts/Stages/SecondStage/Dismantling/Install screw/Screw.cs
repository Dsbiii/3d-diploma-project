using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Dismantling.Install_screw
{
    public class Screw : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] _screws;
        [SerializeField] private bool _isOpen;
        public event System.Action OnChangeAction;

        public bool IsOpen => _isOpen;

        public void Open()
        {
            _isOpen = true;
            foreach (var item in _screws)
            {
                if(item != null)
                    item.enabled = true;
            }
            OnChangeAction?.Invoke();
        }

        public void Close()
        {
            _isOpen = false;
            foreach (var item in _screws)
            {
                if (item != null)
                    item.enabled = false;
            }
            OnChangeAction?.Invoke();
        }
    }
}