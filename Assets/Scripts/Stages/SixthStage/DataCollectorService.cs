using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class DataCollectorService : MonoBehaviour
    {
        [SerializeField] private DataCollectorObject _prefab;
        [SerializeField] private List<DataCollectorObject> _dataCollectorObjects;
        [SerializeField] private Transform _parent;
        [SerializeField] private TMP_InputField _name;
        [SerializeField] private InputField _collectionDepthDay;
        [SerializeField] private InputField _collectionDepthHour;
        [SerializeField] private InputField _collectionDepthMin;
        [SerializeField] private InputField _collectionDepthSec;
        [SerializeField] private InputField _stopWorkDay;
        [SerializeField] private InputField _stopWorkHour;
        [SerializeField] private InputField _stopWorkMin;
        [SerializeField] private InputField _stopWorkSec;
        [SerializeField] private TMP_Text _setting;
        [SerializeField] private TMP_Text _profileSetting;
        [SerializeField] private Toggle _stateToggle;
        [SerializeField] private Toggle _disableToggle;
        [SerializeField] private Toggle _checkDateBaseToggle;
        [SerializeField] private Toggle _loadStatusToggle;
        [SerializeField] private Toggle _tarifToggle;
        [SerializeField] private Toggle _configToggle;
        [SerializeField] private Toggle _statusIOToggle;
        [SerializeField] private Toggle _startAfterServiceToggle;
        [SerializeField] private Button _edit;
        [SerializeField] private Button _delete;
        [SerializeField] private Button _schedule;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _title;
        private Proccess _proccess = Proccess.Add;
        private DataCollectorObject _currentDataCollerctor;
        public void OkButtonClick()
        {
            Check();
            if (_proccess == Proccess.Add)
                Spawn();
            else
            {
                _currentDataCollerctor.SetValue(_name.text, _setting.text, _profileSetting.text, _stateToggle.isOn,
                    _collectionDepthDay.text, _collectionDepthHour.text, _collectionDepthMin.text, _collectionDepthSec.text,
                    _stopWorkDay.text, _stopWorkHour.text, _stopWorkMin.text, _stopWorkSec.text, _disableToggle.isOn,
                    _checkDateBaseToggle.isOn, _loadStatusToggle.isOn, _tarifToggle.isOn, _configToggle.isOn, _statusIOToggle.isOn,
                    _startAfterServiceToggle.isOn);
                _proccess = Proccess.Add;
            }
        }
        private void Spawn()
        {
            DataCollectorObject dataCollectorObject = Instantiate(_prefab, _parent);
            dataCollectorObject.Init(_name.text, _setting.text, _profileSetting.text, _stateToggle.isOn, _edit, _delete, _schedule, this,
                    _collectionDepthDay.text, _collectionDepthHour.text, _collectionDepthMin.text, _collectionDepthSec.text,
                    _stopWorkDay.text, _stopWorkHour.text, _stopWorkMin.text, _stopWorkSec.text, _disableToggle.isOn,
                    _checkDateBaseToggle.isOn, _loadStatusToggle.isOn, _tarifToggle.isOn, _configToggle.isOn, _statusIOToggle.isOn,
                    _startAfterServiceToggle.isOn);
            _dataCollectorObjects.Add(dataCollectorObject);
        }
        public void Clean()
        {
            _name.text = "";
            _setting.text = "Выбрать";
            _profileSetting.text = "Выбрать";
            _stateToggle.isOn = false;
            _collectionDepthDay.text = "";
            _collectionDepthHour.text = "";
            _collectionDepthMin.text = "";
            _collectionDepthSec.text = "";
            _stopWorkDay.text = "";
            _stopWorkHour.text = "";
            _stopWorkMin.text = "";
            _stopWorkSec.text = "";
            _disableToggle.isOn = false;
            _checkDateBaseToggle.isOn = false;
            _loadStatusToggle.isOn = false;
            _tarifToggle.isOn = false;
            _configToggle.isOn = false;
            _statusIOToggle.isOn = false;
            _startAfterServiceToggle.isOn = false;
        }
        public void SelectedObject(DataCollectorObject dataCollectorObject)
        {
            foreach (var item in _dataCollectorObjects)
            {
                if (item != dataCollectorObject)
                    item.Unselect();
            }
        }
        public void SetTitle(string title)
        {
            _title.text += " " + title;
        }
        public void Edit(string name, string setting, string profileSetting, bool state, DataCollectorObject dataCollectorObject,
            string CDDay, string CDHour, string CDMin, string CDSec, string SWDay, string SWHour, string SWMin, string SWSec,
            bool disable, bool checkDB, bool loadStatus, bool tarif, bool config, bool statusIO, bool startAfterService)
        {
            _proccess = Proccess.Edit;
            _name.text = name;
            _setting.text = setting;
            _profileSetting.text = profileSetting;
            _stateToggle.isOn = state;
            _currentDataCollerctor = dataCollectorObject;
            _collectionDepthDay.text = CDDay;
            _collectionDepthHour.text = CDHour;
            _collectionDepthMin.text = CDMin;
            _collectionDepthSec.text = CDSec;
            _stopWorkDay.text = SWDay;
            _stopWorkHour.text = SWHour;
            _stopWorkMin.text = SWMin;
            _stopWorkSec.text = SWSec;
            _disableToggle.isOn = disable;
            _checkDateBaseToggle.isOn = checkDB;
            _loadStatusToggle.isOn = loadStatus;
            _tarifToggle.isOn = tarif;
            _configToggle.isOn = config;
            _statusIOToggle.isOn = statusIO;
            _startAfterServiceToggle.isOn = startAfterService;
            _panel.SetActive(true);
        }
        public void Delete(DataCollectorObject dataCollectorObject)
        {
            _dataCollectorObjects.Remove(dataCollectorObject);
            Destroy(dataCollectorObject.gameObject);
        }
        public void Check()
        {
            if(_name.text != null && _collectionDepthDay.text == "3" && _setting.text == "Фактическая активная мощность за 1 час, кВт" && _profileSetting.text == "Мощность А+ за 30 минут" && _checkDateBaseToggle.isOn && _stateToggle.isOn && _startAfterServiceToggle.isOn && _stopWorkHour.text == "3")
            {
                Debug.Log("Robit");//3-1
            }
        }
        public int GetPoints()
        {
            return _dataCollectorObjects.OrderByDescending(obj => obj.Points()).First().Points();
        }
    }
}