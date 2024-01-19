using Assets.Scripts.Instruments;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.SecondStage.Electric_Box;
using Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.ThirdStage.Phases.CounterPhase
{
    public class PlantObjectPanel : MonoBehaviour
    {
        [SerializeField] private CounterView _counterView;
        [SerializeField] private Ruler _ruler;
        [SerializeField] private GameObject[] _plombs;
        [SerializeField] private CounterPoint[] _counterPoints;
        [SerializeField] private GameObject _counterWork;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _plant;

        private CablesConnector _cablesConnector;
        private GameState _gameState;
        private ThirdStageModel _stageModel;

        private void Awake()
        {
            _plant.onClick.AddListener(Plant);
        }

        public void Init(CablesConnector cablesConnector,GameState gameState, ThirdStageModel thirdStageModel)
        {
            _cablesConnector = cablesConnector;
            _gameState = gameState;
            _stageModel = thirdStageModel;
        }

        public void Plant()
        {
            foreach (var item in _counterPoints)
                item.gameObject.SetActive(false);
            foreach (var item in _plombs)
                item.GetComponent<PlombPoint>().Close();
            _counterWork.SetActive(true);
            _counterWork.transform.position = _stageModel.CurrentTSObject.gameObject.transform.position;
            _counterWork.transform.rotation = _stageModel.CurrentTSObject.gameObject.transform.rotation;
            _stageModel.CurrentTSObject.gameObject.SetActive(false);
            _stageModel.SetupTSOBjects();
            EndPlanting();
            _counterView.OpenLidWithoutScrewdriver();
            _ruler.Close();
            Close();
            _stageModel.PlantedCounter();
        }

        public void StartPlanting()
        {
            foreach (var item in _counterPoints)
                item.gameObject.SetActive(true);
            foreach (var item in _counterPoints)
                item.TryDisplayPoint();
        }

        public void EndPlanting()
        {
            foreach (var item in _counterPoints)
                item.OffPoint();
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