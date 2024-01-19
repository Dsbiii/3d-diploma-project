using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class SelectAbonentPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private List<SelectAbonentObject> _selectAbonentObjects;

        public IEnumerable<SelectAbonentObject> SelectAbonentObjects { get { return _selectAbonentObjects;} }

        public void SetupValue()
        {
            foreach(var item in _selectAbonentObjects)
            {
                if (item.IsSelected)
                {
                    _value.text = item.Value;
                }
            }
        }
        public void Clean()
        {
            foreach (var item in _selectAbonentObjects)
                item.Unselect();
        }

    }
}