using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface
{
    public class NumberButton : MonoBehaviour
    {
        [SerializeField] private int _number;
        [SerializeField] private Button _button;

        public event System.Action<int> OnClicked;

        private void Awake()
        {
            _button.onClick.AddListener(Click);
        }

        public void Click()
        {
            OnClicked?.Invoke(_number);
        }
    }
}