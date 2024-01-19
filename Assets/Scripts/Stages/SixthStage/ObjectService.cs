using Assets.Scripts.Stages.SixStage;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public enum Proccess
    {
        Add,
        Edit
    }
    public class ObjectService : MonoBehaviour
    {
        [SerializeField] private List<SelectPanel> _panels;
        [SerializeField] private Transform _parent;
        [SerializeField] private Button _editBustton;
        [SerializeField] private Button _deleteBustton;
        [SerializeField] private SelectPanel _prefab;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Dropdown _location;
        [SerializeField] private TMP_Dropdown _timeZone;
        [SerializeField] private TMP_InputField _serialNumber;
        [SerializeField] private TMP_InputField _user;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private TMP_InputField _time;
        [SerializeField] private TMP_InputField _connectNumber;
        [SerializeField] private TMP_InputField _dateRelease;
        [SerializeField] private TMP_InputField _dateSetup;
        [SerializeField] private TMP_InputField _dateNextCheck;
        [SerializeField] private TMP_InputField _dateLastCheck;
        [SerializeField] private GameObject _panel;
        [SerializeField] private CreateContextMenuSixStage _createContextMenu;
        [SerializeField] private Toggle _correctToggle;
        public bool IsCorrect;
        private SelectPanel _currentSelectPanel;
        private Proccess _proccess = Proccess.Add;
        public void ClickOKButton()
        {
            CheckToCorrect();
            if (_proccess == Proccess.Add)
                Spawn();
            else
            {
                _currentSelectPanel.SetValue(_name.text, _location.options[_location.value].text, _serialNumber.text, 
                    _dateRelease.text, _location.value, _dateSetup.text, _dateNextCheck.text, _dateLastCheck.text, _user.text, _password.text,
                    _time.text, _connectNumber.text, _timeZone.value);
                _proccess = Proccess.Add;
            }
        }
        public void Spawn()
        {
            Debug.Log("name" +  _name.text);
            Debug.Log("serialNumder" +  _serialNumber.text);
            SelectPanel panel = Instantiate(_prefab, _parent);
            panel.Init(_editBustton, _deleteBustton, _name.text, _location.options[_location.value].text, _serialNumber.text, _dateRelease.text, this, _location.value, _dateSetup.text, _dateNextCheck.text, _dateLastCheck.text, _user.text, _password.text,
                    _time.text, _connectNumber.text, _timeZone.value);
            _panels.Add(panel);
        }
        public void SelectedObject(SelectPanel panel)
        {
            foreach(var item in _panels)
            {
                if(item != panel)
                {
                    item.Unselect();
                }
            }
        }
        public void Delete(SelectPanel panel) 
        {
            if (panel._isActive)
            {
                _panels.Remove(panel);
                Destroy(panel.gameObject);
            }
        }
        public void Edit(string name, string location, string serialNumber, string date, int value, SelectPanel selectPanel, string dateSetup, string dateNextCheck, string dateLastCheck, string user,
            string password, string time, string connectNumber, int timeZoneValue)
        {
            Debug.Log("name" + name + " serial number " + serialNumber);
            _proccess = Proccess.Edit;
            _panel.SetActive(true);
            _name.text = name;
            _location.value = value;
            _serialNumber.text = serialNumber;
            _dateRelease.text = date;
            _currentSelectPanel = selectPanel;
            _time.text = time;
            _connectNumber.text = connectNumber;
            _password.text = password;
            _user.text = user;
            _dateLastCheck.text = dateLastCheck;
            _dateNextCheck.text = dateNextCheck;
            _dateSetup.text = dateSetup;
            _timeZone.value = timeZoneValue;
        }
        public void Clean()
        {
            _name.text = "";
            _location.value = 0;
            _serialNumber.text = "";
            _dateRelease.text = "";
            _time.text = "";
            _connectNumber.text = "";
            _password.text = "";
            _user.text = "";
            _dateLastCheck.text = "";
            _dateNextCheck.text = "";
            _dateSetup.text = "";
            _timeZone.value = 0;
            Debug.Log(_name.text + " " + _serialNumber.text);
        }
        public void CreateContextMenu()
        {
            if(_createContextMenu != null)
                _createContextMenu.CreateContextMenu();
        }
        public void CheckToCorrect()
        {
            Debug.Log("Stisn ba zaebal");
            if (_name.text == "СЭТ-4ТМ.03М" && _serialNumber.text == "0112055629" && _connectNumber.text == "+79273266002" && _location.value == 5 && _timeZone.value == 3)
            {
                Debug.Log("Robit");//2-1
                IsCorrect = true;
            }
        }
    }
}