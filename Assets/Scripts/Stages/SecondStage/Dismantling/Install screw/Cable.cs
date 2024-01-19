using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Dismantling.Install_screw
{
    public class Cable : MonoBehaviour
    {
        [SerializeField] private GameObject _cableTattered;
        private MeshRenderer _cable;
        public event System.Action OnChangeAction;
        public bool IsPullOut { get; private set; }

        private void Awake()
        {
            _cable = GetComponent<MeshRenderer>();
        }

        public void Off()
        {
            _cableTattered.SetActive(false);
            IsPullOut = false;
            _cable.enabled = false;
            OnChangeAction?.Invoke();
        }

        public void PullOutCable()
        {
            _cableTattered.SetActive(true);
            IsPullOut = true;
            _cable.enabled = false;
            OnChangeAction?.Invoke();
        }

    }
}