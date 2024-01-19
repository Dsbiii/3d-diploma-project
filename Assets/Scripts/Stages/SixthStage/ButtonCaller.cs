using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class ButtonCaller : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public void Call()
        {
            _button.onClick.Invoke();
        }
    }
}