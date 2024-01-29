using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage.Directories
{
    public class DirectoriesPanel : MonoBehaviour
    {
        [SerializeField] private List<EquipmentObjectType> _equipmentObjectTypes;
        [SerializeField] private List<EquipmentObjectType> _equipmentObjectTypes2;
        [SerializeField] private Button _addRoute;
        [SerializeField] private ButtonService _buttonService;
        [SerializeField] private TMP_Text _deviceName;
        public List<EquipmentObjectType> EquipmentObjectTypes => _equipmentObjectTypes;
        public List<EquipmentObjectType> EquipmentObjectTypes2 => _equipmentObjectTypes2;
        public EquipmentObjectType CurrentSelectedObject;
        public void AddEquipmentObjectType(EquipmentObjectType equipmentObjectType)
        {
            _equipmentObjectTypes.Add(equipmentObjectType);
            equipmentObjectType.OnSelected += Select;
            equipmentObjectType.isSelected += SetButtonState;
        }
        public void RemoveEquipmentObject(EquipmentObjectType equipmentObjectType)
        {
            _equipmentObjectTypes.Remove(equipmentObjectType);
        }
        public void AddEquipmentObject(EquipmentObjectType equipmentObjectType)
        {
            _equipmentObjectTypes2.Add(equipmentObjectType);
            equipmentObjectType.OnSelected += Select;
            equipmentObjectType.isSelected += SetAddButtonRoute;
        }
        public void Select(EquipmentObjectType equipmentObjectType)
        {
            foreach (var item in _equipmentObjectTypes)
            {
                if (item != equipmentObjectType)
                {
                    item.Unselect();
                }
            }
            foreach (var item in _equipmentObjectTypes2)
            {
                if (item != equipmentObjectType)
                {
                    item.Unselect();
                }
            }
            CurrentSelectedObject = equipmentObjectType;
            _deviceName.text = equipmentObjectType.Name;
        }
        public bool TryGetCurrentSelectedObject(out EquipmentObjectType equipmentObjectType)
        {
            if (CurrentSelectedObject != null)
            {
                equipmentObjectType = CurrentSelectedObject;
                return true;
            }
            equipmentObjectType = null;
            return false;
        }
        private void SetButtonState(bool state)
        {
            if (state)
            {
                _buttonService.SelectObject();
            }
            else
            {
                _buttonService.DeselectObject();
            }
        }
        public void SetDeviceName()
        {
            foreach (var item in _equipmentObjectTypes2)
            {
                if (item.IsSelected)
                {
                    _deviceName.text = item.Name;
                }
            }
        }
        public void Clean()
        {
            _deviceName.text = "";
        }
        public void Deselect()
        {
            foreach(var item in _equipmentObjectTypes)
            {
                item.Unselect();
            }
        }
        private void SetAddButtonRoute(bool state)
        {
            _addRoute.interactable = state;
        }
    }
}