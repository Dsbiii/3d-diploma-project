using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Stages.SixthStage
{
    public class CheckGameMode : MonoBehaviour
    {
        [Inject] private GameMode _gameMode;
        [SerializeField] private Button[] _buttons;
        private void OnEnable()
        {
            if(_gameMode.IsDemo) 
            {
                foreach(var button in _buttons)
                {
                    button.interactable = false;
                }
            }
        }
    }
}