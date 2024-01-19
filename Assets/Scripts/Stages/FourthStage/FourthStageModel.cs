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

        public IEnumerable<Item> Items => _obligatoryItemsForFourth;
        public IEnumerable<Item> ItemsToRemove => _obligatoryItemsToRemove;
        public MovableObject SelectedMovableObject { get; private set; }
        private Item _selectedItem;
        [Inject] private Inventory _inventory;
        public bool IsExitedFromTP { get; private set; }

        public bool IsPickedCottonGlovesInspectionStage { get; private set; }
        public bool IsPickedHelmetInspectionStage { get; private set; }
        public bool IsPickedGlassesInspectionStage { get; private set; }

        public void ExitFromTP()
        {
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