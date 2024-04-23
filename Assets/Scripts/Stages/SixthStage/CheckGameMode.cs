using Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector;
using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
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
        [SerializeField] private LaptopCablePoint _laptopCablePoint;
        [SerializeField] private CounterCablePoint _counterCablePoint;
        private void OnEnable()
        {
            if (_gameMode.IsDemo)
            {
                foreach (var button in _buttons)
                {
                    button.interactable = false;
                }
                _USPD.gameObject.SetActive(true);
                _controller.gameObject.SetActive(true);
                _USPD.PlantWithoutFlag();
                _controller.PlantWithoutFlag();
                //_laptopCablePoint.SetupPoint();
                //_counterCablePoint.SetupPoint();
                _antena.SetupAntena();
            }
        }
    }
}