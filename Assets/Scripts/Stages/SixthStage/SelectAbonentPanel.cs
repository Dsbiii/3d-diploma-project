﻿using System.Collections;
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
        public int ItemIndex;

        public IEnumerable<SelectAbonentObject> SelectAbonentObjects { get { return _selectAbonentObjects;} }

        public void SetupValue()
        {
            foreach(var item in _selectAbonentObjects)
            {
                if (item.IsSelected)
                {
                    ItemIndex = _selectAbonentObjects.IndexOf(item);
                    _value.text = item.Value;
                }
            }
        }
        public void OnObject(string text)
        {
            foreach (var item in _selectAbonentObjects)
                if (item.Value == text)
                    item.Select();
        }
        public void Clean()
        {
            foreach (var item in _selectAbonentObjects)
                item.Unselect();
        }
        private void OnEnable()
        {
            if (_prefab != null)
            {
                foreach (var item in _panel.GetText())
                {
                    SelectAbonentObject selectAbonent = Instantiate(_prefab, _parent);
                    selectAbonent.Init(item, this);
                    _selectAbonentObjects.Add(selectAbonent);
                }
            }
        }
        private void OnDisable()
        {
            for (int i = _selectAbonentObjects.Count - 1; i >= 6; i--)
            {
                Destroy(_selectAbonentObjects[i].gameObject);
                _selectAbonentObjects.RemoveAt(i);
            }
        }
    }
}