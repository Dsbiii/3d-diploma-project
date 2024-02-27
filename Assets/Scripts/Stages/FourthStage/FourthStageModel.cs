using Assets.Scripts.Stages.FourthStage.CablesSystem;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class FourthStageModel : MonoBehaviour
    {
        [SerializeField] private FourthStagePlomb _transformatorFourthStagePlombs;
        [SerializeField] private FourthStagePlomb[] _ikkCounterFourthStagePlombs;

        [SerializeField] private List<Item> _obligatoryItemsForFourth;
        [SerializeField] private List<Item> _obligatoryItemsToRemove;
        [SerializeField] private AntenaPoint _antenaPoint;
        [SerializeField] private MovableObject _controller;
        [SerializeField] private MovableObject _uspd;
        [SerializeField] private FourthStageCablePanel _fourthStageCablePanel;
        [SerializeField] private FourthStagePlomb _fourthStagePlomb;
        [SerializeField] private SIMPoint _firstSIMPoint;
        [SerializeField] private SIMPoint _secondSIMPoint;

        public IEnumerable<Item> Items => _obligatoryItemsForFourth;
        public IEnumerable<Item> ItemsToRemove => _obligatoryItemsToRemove;
        public MovableObject SelectedMovableObject { get; private set; }
        private Item _selectedItem;
        [Inject] private Inventory _inventory;
        [Inject] private FourthStageExamSystem _fourthStageExamSystem;
        public bool IsExitedFromTP { get; private set; }

        public bool IsPickedCottonGlovesInspectionStage { get; private set; }
        public bool IsPickedHelmetInspectionStage { get; private set; }
        public bool IsPickedGlassesInspectionStage { get; private set; }

        public void ExitFromTP()
        {
            Debug.Log("_controller.IsPlanted " + _controller.IsPlanted);
            Debug.Log("_uspd.IsPlanted " + _uspd.IsPlanted);
            Debug.Log("Plombs " + (_transformatorFourthStagePlombs.IsSetupedPlomb ||
                _ikkCounterFourthStagePlombs.Where(item => item.IsSetupedPlomb).ToArray().Length == _ikkCounterFourthStagePlombs.Length));
            Debug.Log("Sims " + (_firstSIMPoint.IsIndicated || _secondSIMPoint.IsIndicated));
            Debug.Log("_antenaPoint.IsSetupedAntena " + _antenaPoint.IsSetupedAntena);

            if(_controller.IsPlanted &&
                _uspd.IsPlanted &&
                (_transformatorFourthStagePlombs.IsSetupedPlomb ||
                _ikkCounterFourthStagePlombs.Where(item => item.IsSetupedPlomb).ToArray().Length == _ikkCounterFourthStagePlombs.Length)
                && (_firstSIMPoint.IsIndicated || _secondSIMPoint.IsIndicated) &&
                _antenaPoint.IsSetupedAntena)
            {
                _fourthStageExamSystem.SetRightExitFromTP();
            }

    //        if (_antenaPoint.IsSetupedAntena && _fourthStageCablePanel.IsSetuped
    //&& _fourthStagePlomb.IsSetupedPlomb &&
    //_controller.IsPlanted &&
    //_uspd.IsPlanted &&
    //(_firstSIMPoint.IsIndicated && _secondSIMPoint.IsIndicated))
    //        {
    //            _fourthStageExamSystem.SetRightExitFromTP();
    //        }
            IsExitedFromTP = true;
        }

        public void TakedPickedCottonGlovesInspectionStage()
        {
            IsPickedCottonGlovesInspectionStage = true;
        }
        public void TakedPickedGlassesInspectionStage()
        {
            IsPickedGlassesInspectionStage = true;
        }
        public void TakedPickedHelmetInspectionStage()
        {
            IsPickedHelmetInspectionStage = true;
        }

        public void SetSelectedMovableObject(MovableObject movableObject)
        {
            if (SelectedMovableObject != null)
                SelectedMovableObject.OffPoints();
            SelectedMovableObject = movableObject;
            SelectedMovableObject.OnPoints();
        }

        public void SelectItem(Item item)
        {
            _selectedItem = item;
        }

        public void PlantObject()
        {
            if(_selectedItem != null)
                _inventory.RemoveItem(_selectedItem);

            SelectedMovableObject.Plant();
            SelectedMovableObject = null;
        }


    }
}