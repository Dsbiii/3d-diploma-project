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
        [SerializeField] private SelectAbonentObject _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private AbonentPanel _panel;

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
        private void OnEnable()
        {
            foreach(var item in _panel.GetText())
            {
                SelectAbonentObject selectAbonent = Instantiate(_prefab, _parent);
                selectAbonent.Init(item, this);
                _selectAbonentObjects.Add(selectAbonent);
            }
        }
        private void OnDisable()
        {
            for(int i = 7; i < _selectAbonentObjects.Count; i++)
            {
                Destroy(_selectAbonentObjects[i].gameObject);
                _selectAbonentObjects.RemoveAt(i);
            }
        }
    }
}