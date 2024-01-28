using Assets.Scripts.Stages.SixthStage.Tools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class RouteEquimentPanel : MonoBehaviour
    {
        [SerializeField] private Button _edit;
        [SerializeField] private Button _delete;
        [SerializeField] private TypeConnection _connectionType;
        [SerializeField] private TMP_InputField _ipAdress;
        [SerializeField] private TMP_InputField _port;
        [SerializeField] private List<RouteEquipment> _equipments;
        [SerializeField] private RouteEquipment _prefab;
        [SerializeField] private Transform _parent;
        private RouteEquipment _currentEquiment;
        public void Spawn()
        {
            RouteEquipment equip = Instantiate(_prefab, _parent);
            equip.Init(_connectionType.GetType(), _connectionType.Priority, _edit, _delete, this);
            _equipments.Add(equip);
        }
        public void Selected(RouteEquipment routeEquipment)
        {
            foreach(var equip in _equipments)
            {
                if (equip != routeEquipment)
                {
                    equip.Unselect();
                }
            }
        }
        public void SetValue()
        {
            _currentEquiment.SetValue(_port.text, _ipAdress.text);
        }
        public void Edit(RouteEquipment equip, string port, string IPadress)
        {
            _port.text = port;
            _ipAdress.text = IPadress;
            _currentEquiment = equip;
        }
        public void Delete(RouteEquipment equip)
        {
            _equipments.Remove(equip);
            if(!equip.IsSelected)
                Destroy(equip.gameObject);
        }
        public void Clean()
        {
            _port.text = "";
            _ipAdress.text = "";
        }
    }
}