using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage
{
    public class SecondStageModel
    {
        private List<Item> _items = new List<Item>();
        public bool IsTakedScrewdriver { get; private set; }
        public bool IsFristSelectedCaps { get; private set; }
        public bool IsTakedCaps { get; private set; }
        public bool IsTakedPlombs { get; private set; }
        public IEnumerable<Item> Items => _items;
        private List<GameObject> _screwDrivers = new List<GameObject>();



        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void PutAwayScrewDrivers()
        {
            IsTakedScrewdriver = false;
            foreach (var item in _screwDrivers)
                item.SetActive(true);
        }

        public void PutAwayPlombs()
        {
            IsTakedPlombs = false;
        }

        public void TakeCaps()
        {
            IsTakedCaps = true;
            IsFristSelectedCaps = true;
        }

        public void PutAwayCups()
        {
            IsTakedCaps = false;
        }

        public void TakePlombs()
        {
            IsTakedPlombs = true;
        }

        public void TakeScrewDriver(GameObject screwDriver)
        {
            _screwDrivers.Add(screwDriver);
            SecondStageController.Instance.AddExamPassed("IsTakedScrewdriver");
            IsTakedScrewdriver = true;
        }
    }
}