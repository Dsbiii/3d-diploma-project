using Assets.Scripts.Stages.FifthStage.Services.CouterCableConnector;
using Assets.Scripts.Stages.FifthStage.Services.LaptopCableConnector;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage
{
    public class SATPanelObject : MonoBehaviour
    {
        [SerializeField] private LaptopCablePoint _laptopCablePoint;
        [SerializeField] private CounterCablePoint _counterCablePoint;
        [SerializeField] private FifthStageModel _fifthStageModel;
        [SerializeField] private GameObject[] _gameObjects;

        private void OnEnable()
        {
            if(_fifthStageModel.IsRightConnectedComputer && _laptopCablePoint.IsIndicated &&
                _counterCablePoint.IsIndicated)
            {
                foreach(var item in _gameObjects)
                {
                    item.SetActive(true);
                }
            }
            else
            {
                foreach(var item in _gameObjects)
                {
                    item.SetActive(false);
                }
            }
        }
    }
}