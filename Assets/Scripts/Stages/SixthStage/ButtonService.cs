using Assets.Scripts.Stages.SixthStage.Directories;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class ButtonService : MonoBehaviour
    {
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _editButton;
        [SerializeField] private GameObject _addPanel;
        [SerializeField] private CreateSMPanel _editPanel;
        [SerializeField] private DirectoriesPanel _directoriesPanel;
        
        private void Start()
        {
            _addButton.onClick.AddListener(Add);
            _deleteButton.onClick.AddListener(Delete);
            _editButton.onClick.AddListener(Edit);
        }
        private void Add()
        {
            _addPanel.SetActive(true);
        }
        private void Delete()
        {
            if(_directoriesPanel.TryGetCurrentSelectedObject(out EquipmentObjectType equipmentObjectType))
            {
                Destroy(equipmentObjectType.gameObject);
                _directoriesPanel.RemoveEquipmentObject(equipmentObjectType);
                DeselectObject();
            }
        }
        private void Edit()
        {
            _editPanel.OpenForEdit(_directoriesPanel.CurrentSelectedObject.Name, _directoriesPanel.CurrentSelectedObject.SerialNumber, _directoriesPanel.CurrentSelectedObject.PlaceNumber, _directoriesPanel.CurrentSelectedObject.ValuePlacePoint, _directoriesPanel.CurrentSelectedObject.User, _directoriesPanel.CurrentSelectedObject.Password, _directoriesPanel.CurrentSelectedObject.TimeZone, _directoriesPanel.CurrentSelectedObject.ValueTimePoint);

        }
        public void SelectObject()
        {
            _editButton.interactable = true;
            _deleteButton.interactable = true;
        }
        public void DeselectObject()
        {
            _editButton.interactable = false;
            _deleteButton.interactable = false;
        }
    }
}