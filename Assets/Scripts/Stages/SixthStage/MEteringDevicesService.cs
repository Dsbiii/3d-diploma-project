using Assets.Scripts.Stages.SixthStage.Tools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class MEteringDevicesService : MonoBehaviour
    {
        [SerializeField] private List<MeteringDevicesObject> _objects;
        [SerializeField] private TMP_Text _deviceName;
        [SerializeField] private Transform _parent;
        [SerializeField] private TMP_Text _port;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private MeteringDevicesObject _prefab;
        [SerializeField] private Button _edit;
        [SerializeField] private Button _delete;
        [SerializeField] private ChanelForming _routePrefab;
        [SerializeField] private TypeConnection _typeConnection;
        [SerializeField] private List<ChanelForming> _routes;
        [SerializeField] private Transform _parentRoute;
        [SerializeField] private ObjectService _service;
        [SerializeField] private TMP_Text _titleEdit;
        private ChanelForming _currentRoute;

        public void Spawn()
        {
            ChanelForming chanelForming = Instantiate(_routePrefab, _parentRoute);
            chanelForming.Init(_typeConnection.GetType(), _typeConnection.Priority, _edit, _delete, this, _deviceName.text);
            _routes.Add(chanelForming);

        }
        public void Selected(MeteringDevicesObject obj)
        {
            foreach (MeteringDevicesObject item in _objects)
            {
                if (item != obj)
                {
                    item.Unselect();
                }
            }
        }
        public void Selected(ChanelForming chanelForming)
        {
            foreach(var item in _routes)
            {
                if(item != chanelForming)
                {
                    item.Unselect();
                }
            }
        }
        public void SetValue()
        {
            _currentRoute.SetupValues(_port.text, _inputField.text);
        }
        public void SetDeviceName(string deviceName)
        {
            _deviceName.text = deviceName;
        }
        private void OnEnable()
        {
            _deviceName.text = "";
            for (int i = 0; i < _service.GetPanels().Count; i++)
            {
                MeteringDevicesObject meteringDevices = Instantiate(_prefab, _parent);
                meteringDevices.Init(_service.GetPanels()[i].Name, this);
                _objects.Add(meteringDevices);
            }
        }
        private void OnDisable()
        {
            for (int i = 5; i < _objects.Count; i++)
            {
                Destroy(_objects[i].gameObject);
                _objects.RemoveAt(i);
            }
            foreach (var obj in _objects)
            {
                obj.Unselect();
            }
        }
        public void ClearDeviceName()
        {
            _deviceName.text = "";
        }
        public void Edit(ChanelForming chanelForming)
        {
            _currentRoute = chanelForming;
        }
        public void SetEditTitle(string title)
        {
            _titleEdit.text = title + " - Редактирование";
        }
        public void Delete(ChanelForming chanel)
        {
            Destroy(chanel.gameObject);
            _routes.Remove(chanel);
        }
        public void CleanEditPanel()
        {
            _port.text = "Выбрать...";
            _inputField.text = "";

        }
        public int GetPoints()
        {
            if(_routes.Count == 0)
                return 0;
            return _routes.OrderByDescending(obj => obj.SumPoints()).First().SumPoints();
        }
    }
}