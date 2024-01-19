using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage.Tools
{
    public class TypeConnection : MonoBehaviour , IPointerClickHandler
    {
        [SerializeField] private List<TypeConnection> _typeConnections;
        [SerializeField] private Image _image;
        [SerializeField] private Color _color;
        [SerializeField] private TMP_Dropdown _priority;
        [SerializeField] private TMP_Text _type;
        [SerializeField] private ChanelForming _route;
        [SerializeField] private CheckToTheRight _checkToTheRight;
        [SerializeField] private TypeConnection _correctConnection;
        public string Priority => _priority.options[_priority.value].text;
        public string Type => _type.text;
        public bool IsSelected;
        private Color _baseColor;

        private void Awake()
        {
            _baseColor = _image.color;
        }

        public void Selected()
        {
            _image.color = _color;
            IsSelected = true;
        }

        public void Unselected()
        {
            _image.color = _baseColor;
            IsSelected = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            foreach(var typeConnection in _typeConnections)
            {
                typeConnection.Unselected();
            }
            Selected();
        }
        public void Clean()
        {
            foreach (var typeConnection in _typeConnections)
                typeConnection.Unselected();
            if(_priority != null)
                _priority.value = 0;
        }
        public string GetType()
        {
            foreach( var typeConnection in _typeConnections)
            {
                if (typeConnection.IsSelected)
                {
                    return typeConnection.Type;
                }
            }
            return null;
        }
    }
}