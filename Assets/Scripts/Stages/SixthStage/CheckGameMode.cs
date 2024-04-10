using Assets.Scripts.Stages.FourthStage;
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
        [SerializeField] private MovableObject _USPD;
        [SerializeField] private MovableObject _controller;
        [SerializeField] private AntenaPoint _antena;
        private void OnEnable()
        {
            if (_gameMode.IsDemo)
            {
                foreach (var button in _buttons)
                {
                    button.interactable = false;
                }
                Debug.Log("Stisn ba Zaebal");
                _USPD.gameObject.SetActive(true);
                _controller.gameObject.SetActive(true);
                _USPD.PlantWithoutFlag();
                _controller.PlantWithoutFlag();
                _antena.SetupAntena();
            }
        }
    }
}