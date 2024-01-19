using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases.CounterPhase
{
    public class CountersPanel : MonoBehaviour
    {
        [SerializeField] private Ruler _ruler;
        [SerializeField] private GameObject _panel;
        [SerializeField] private CounterOption[] _counterOption;
        [SerializeField] private Transform _counterStartPosition;
        private ThirdStageModel _thirdStageModel;
        private PlantObjectPanel _palantObjectPanel;

        private void Awake()
        {
            foreach (var item in _counterOption)
                item.OnClicked += SelectCounter;

            int rnd = Random.Range(3, 6);
            for (int i = 0; i < rnd; i++)
            {

                foreach (var option in _counterOption)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        option.transform.SetAsFirstSibling();
                    }
                    else
                    {
                        option.transform.SetAsLastSibling();
                    }
                }
            }
        }

        public void Init(ThirdStageModel thirdStageModel, GameState gameState, PlantObjectPanel palantObjectPanel)
        {
            _thirdStageModel = thirdStageModel;
            _palantObjectPanel = palantObjectPanel;
        }

        public void SelectCounter(CounterOption counterOption)
        {
            if (counterOption.IsRight)
                _thirdStageModel.IsRightSelectedCounter = true;
            var counter = Instantiate(counterOption.Counter, _counterStartPosition.position, Quaternion.Euler(-90, 0, 90));
            _thirdStageModel.SetSelectingTSObjects(counter);
            _ruler.Open();
            _ruler.SetCounter(counter.transform);
            _palantObjectPanel.StartPlanting();
            Close();
        }

        public void Open()
        {
            //_gameState.EnterInSelectTCObjectState();
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }

    }
}