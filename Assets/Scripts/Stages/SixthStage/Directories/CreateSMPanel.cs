using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage.Directories
{
    public class CreateSMPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_InputField _serialNumber;
        [SerializeField] private TMP_InputField _user;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private TMP_InputField _timeZone;
        [SerializeField] private TMP_Dropdown _placePoint;
        [SerializeField] private TMP_Dropdown _timePoint;

        [SerializeField] private GameObject _panel;
        [SerializeField] private ObjectTypeDirectoryPanel _objectTypeDirectoryPanel;
        [SerializeField] private EquipmentObjectType _equipmentObjectTypePrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _parent2;
        [SerializeField] private DirectoriesPanel _directoriesPanel;
        private bool _correctCreate;
        public void OpenPanel()
        {
            _panel.SetActive(true);
            if (_objectTypeDirectoryPanel.SelectedObjectType != null)
            {
                _name.text = _objectTypeDirectoryPanel.SelectedObjectType.Name;
                _titleText.text = _objectTypeDirectoryPanel.SelectedObjectType.Name;
            }
        }

        public void AddObjectType()
        {
            EquipmentObjectType equipment = Instantiate(_equipmentObjectTypePrefab, _parent);
            //EquipmentObjectType equipment2 = Instantiate(_equipmentObjectTypePrefab, _parent2);
            equipment.Init(_name.text, _name.text, _serialNumber.text, _placePoint.options[_placePoint.value].text, _user.text, _password.text, _timeZone.text, _timePoint.options[_timePoint.value].text, _placePoint.value, _timePoint.value);
            //equipment2.Init(_name.text, _name.text, _serialNumber.text, _placePoint.options[_placePoint.value].text);
            _directoriesPanel.AddEquipmentObjectType(equipment);
            //_directoriesPanel.AddEquipmentObject(equipment2);
            CheckCorrect();
        }

        public void EditObject()
        {
            if (_directoriesPanel.CurrentSelectedObject != null)
            {
                _directoriesPanel.CurrentSelectedObject.Init(_name.text, _name.text, _serialNumber.text, _placePoint.options[_placePoint.value].text, _user.text, _password.text, _timeZone.text, _timePoint.options[_timePoint.value].text, _placePoint.value, _timePoint.value);

                Debug.Log("pass" + _password.text);
            }
        }
        public void CreateForSecond()
        {
            foreach(var item in _directoriesPanel.EquipmentObjectTypes2)
            {
                Destroy(item.gameObject);
            }
            _directoriesPanel.EquipmentObjectTypes2.Clear();
            for (int i = 0; i < _directoriesPanel.EquipmentObjectTypes.Count; i++)
            {
                EquipmentObjectType equipmentObject = Instantiate(_equipmentObjectTypePrefab, _parent2);
                equipmentObject.Init(_directoriesPanel.EquipmentObjectTypes[i].Name, _directoriesPanel.EquipmentObjectTypes[i].Type, _directoriesPanel.EquipmentObjectTypes[i].SerialNumber, _directoriesPanel.EquipmentObjectTypes[i].PlaceNumber, _directoriesPanel.EquipmentObjectTypes[i].User, _directoriesPanel.EquipmentObjectTypes[i].Password, _directoriesPanel.EquipmentObjectTypes[i].TimeZone, _directoriesPanel.EquipmentObjectTypes[i].TimePoint, _directoriesPanel.EquipmentObjectTypes[i].ValuePlacePoint, _directoriesPanel.EquipmentObjectTypes[i].ValueTimePoint);
                _directoriesPanel.AddEquipmentObject(equipmentObject);
            }
        }
        public void OpenForEdit(string name, string? serialNumber, string placePoint, int placePointvalue, string user, string password, string timeZone, int valueTimePoint)
        {
            _panel.SetActive(true);
            _titleText.text = name;
            _name.text = name;
            _serialNumber.text = serialNumber;
            _placePoint.value = placePointvalue;
            _user.text = user;
            _password.text = password;
            _timeZone.text = timeZone;
            _timePoint.value = valueTimePoint;
        }
        public void CleanPanel()
        {
            _serialNumber.text = "";
            _user.text = "";
            _password.text = "";
            _timeZone.text = "";
            _placePoint.value = 0;
            _timePoint.value = 0;
        }
        private void CheckCorrect()
        {
            if (_serialNumber.text == "011994" &&  _user.text == "admin" &&  _password.text == "12345" && _name.text == "SM160" && _placePoint.value == 5 && _timePoint.value == 1) 
            {
                Debug.Log("Robit");//1-1
                _correctCreate = true;
            }
        }
    }
}