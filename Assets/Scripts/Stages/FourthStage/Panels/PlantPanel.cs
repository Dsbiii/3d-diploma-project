using Assets.Scripts.Stages.ThirdStage.Phases.CounterPhase;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage.Panels
{
    public class PlantPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        public CounterPoint CounterPoint { get; private set; }
        [Inject] private FourthStageModel _fourthStageModel;


        public void SetSelectedPlant(CounterPoint counterPoint)
        {
            CounterPoint = counterPoint;
        }

        public void Plant()
        {
            _fourthStageModel.PlantObject();
            if(CounterPoint != null)
                CounterPoint.Plant();
            Close();
        }

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