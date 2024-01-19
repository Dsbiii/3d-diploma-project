using Assets.Scripts.Stages.FourthStage.Panels;
using Assets.Scripts.Stages.ThirdStage.Phases.CounterPhase;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class MovableObject : MonoBehaviour
    {
        [SerializeField] private Transform _rightPoint;
        [SerializeField] private CounterPoint[] _counterPoints;
        public bool IsPlanted { get; private set; }

        [Inject] private FourthStageModel _model;
        [Inject] private FourthStageExamSystem _system;
        [Inject] private PlantPanel _plantPanel;

        public void OnPoints()
        {
            foreach(var item in _counterPoints)
            {
                item.TryDisplayPoint();
            }
        }

        public void OffPoints()
        {
            foreach (var item in _counterPoints)
            {
                item.OffPoint();
            }
        }

        public void Plant()
        {
            if(_plantPanel.CounterPoint != null)
                transform.position = _rightPoint.position;

            if (_model.IsExitedFromTP)
            {
                _system.SetRightExitFromTP(false);
            }

            IsPlanted = true;
            OffPoints();
        }

        public void PlantWithoutFlag()
        {
            transform.position = _rightPoint.position;
            //if (_model.IsExitedFromTP)
            //{
            //    _system.SetRightExitFromTP(false);
            //}

            OffPoints();
        }

    }
}