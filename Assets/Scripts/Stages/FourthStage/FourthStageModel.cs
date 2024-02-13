using Assets.Scripts.Stages.FourthStage.CablesSystem;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class FourthStageModel : MonoBehaviour
    {
        [SerializeField] private List<Item> _obligatoryItemsForFourth;
        [SerializeField] private List<Item> _obligatoryItemsToRemove;

        [SerializeField] private MovableObject _controller;
        [SerializeField] private MovableObject _uspd;
        [SerializeField] private AntenaPoint _antenaPoint;
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
            if(_antenaPoint.IsSetupedAntena && _fourthStageCablePanel.IsSetuped
                && _fourthStagePlomb.IsSetupedPlomb &&
                _controller.IsPlanted &&
                _uspd.IsPlanted &&
                (_firstSIMPoint.IsIndicated && _secondSIMPoint.IsIndicated))
            {
                _fourthStageExamSystem.SetRightExitFromTP();
            }
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