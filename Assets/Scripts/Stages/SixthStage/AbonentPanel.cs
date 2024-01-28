using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class AbonentPanel : MonoBehaviour
    {
        [SerializeField] private List<AbonentObject> _panels;
        [SerializeField] private Transform _parent;
        [SerializeField] private Button _editBustton;
        [SerializeField] private Button _deleteBustton;
        [SerializeField] private AbonentObject _prefab;
        [SerializeField] private TMP_InputField _serialNumberValue;
        [SerializeField] private TMP_InputField _nameValue;
        [SerializeField] private TMP_InputField _surnameValue;
        [SerializeField] private TMP_InputField _lastNameValue;
        [SerializeField] private TMP_Dropdown _typeValue;
        [SerializeField] private GameObject _panel;
        [SerializeField] private RegisterAbonent _registerAbonent;
        private Proccess _proccess = Proccess.Add;
        private AbonentObject _currentAbonentObject;
        public void ClickOKButton()
        {
            if (_proccess == Proccess.Add)
                Spawn();
            else
            {
                _currentAbonentObject.SetValue(_nameValue.text, _serialNumberValue.text, _surnameValue.text, _lastNameValue.text, _typeValue.options[_typeValue.value].text, _typeValue.value);
                _proccess = Proccess.Add;
            }
        }
        public void Spawn()
        {
            AbonentObject panel = Instantiate(_prefab, _parent);
            panel.Init(_nameValue.text, _serialNumberValue.text, _surnameValue.text, _lastNameValue.text, _typeValue.options[_typeValue.value].text, _editBustton, _deleteBustton, this, _typeValue.value);
            Debug.Log(_typeValue.value + " value");
            _panels.Add(panel);
        }
        public void SelectedObject(AbonentObject panel)
        {
            foreach (var item in _panels)
            {
                if (item != panel)
                {
                    item.Unselect();
                }
            }
        }
        public void Delete(AbonentObject abonentObject)
        {
            Destroy(abonentObject.gameObject);
            _panels.Remove(abonentObject);
        }
        public void Clean()
        {
            _serialNumberValue.text = "";
            _nameValue.text = "";
            _lastNameValue.text = "";
            _surnameValue.text = "";
            _typeValue.value = 0;
        }
        public void Edit(string name, string serialNumber, string surname, string lastName, string type, AbonentObject abonentObject, int value)
        {
            _proccess = Proccess.Edit;
            _panel.SetActive(true);
            _serialNumberValue.text = serialNumber;
            _nameValue.text = name;
            _lastNameValue.text = lastName;
            _surnameValue.text = surname;
            _typeValue.value = value;
            _currentAbonentObject = abonentObject;
        }
        public List<string> GetText()
        {
            List<string> text = new List<string>();
            foreach(AbonentObject abonentObject in _panels)
            {
                text.Add(abonentObject.Surname + " " + abonentObject.Name + " " + abonentObject.LastName + " " + abonentObject.SerialNumber);
            }
            return text;
        }
    }
}