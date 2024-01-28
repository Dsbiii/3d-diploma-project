using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage.Directories
{
    public class ObjectTypeDirectoryPanel : MonoBehaviour
    {
        [SerializeField] private List<ObjectType> _objectTypes;
        [SerializeField] private DirectoriesPanel _directoriesPanel;
        [SerializeField] private TMP_InputField _search;
        public ObjectType SelectedObjectType { get; private set; }

        private void Awake()
        {
            foreach (ObjectType type in _objectTypes)
            {
                type.OnClick += SelectObjectType;
            }
        }

        public void SelectObjectType(ObjectType objectType)
        {
            if(SelectedObjectType == objectType)
            {
                SelectedObjectType.Unselect();
                SelectedObjectType = null;
                return;
            }
            if (SelectedObjectType != null)
                SelectedObjectType.Unselect();
            SelectedObjectType = objectType;
            SelectedObjectType.Select();

        }
        public void CleanPanel()
        {
            foreach (ObjectType type in _objectTypes)
            {
                type.Unselect();
            }
            _search.text = "";
        }
    }
}