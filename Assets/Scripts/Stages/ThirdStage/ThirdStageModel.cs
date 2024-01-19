using Assets.Scripts.Stages.ThirdStage.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage
{
    public class ThirdStageModel : MonoBehaviour
    {
        private List<Item> _items = new List<Item>();
        public bool IsTakedScrewdriver { get; private set; }
        public IEnumerable<Item> Items => _items;
        public TSObject CurrentTSObject { get; private set; }

        public bool IsPlantedCounter { get; private set; }
        public bool IsPlatedIKK { get; private set; }
        public bool IsPlantedTransformers { get; private set; }
        public bool IsSetupedCables { get; private set; }

        public bool IsRightSelectedCounter { get; set; }
        public bool IsRightSelectedTransformers { get; set; }
        public bool IsRightSelectedCables { get; set; }

        public bool IsMarkered { get; private set; }
        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void PlantedCounter()
        {
            IsPlantedCounter = true;
        }

        public void PlantedIKK()
        {
            IsPlatedIKK = true;
        }

        public void Markered()
        {
            IsMarkered = true;
        }

        public void PlatedTransformers()
        {
            IsPlantedTransformers = true;
        }

        public void SetupCables()
        {
            IsSetupedCables = true;
        }

        public void TakeScrewDriver()
        {
            IsTakedScrewdriver = true;
        }

        public void SetSelectingTSObjects(TSObject tSObject)
        {
            CurrentTSObject = tSObject;
        }

        public void SetupTSOBjects()
        {
            CurrentTSObject = null;
        }
    }
}