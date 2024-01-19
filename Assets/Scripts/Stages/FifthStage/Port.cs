using Assets.Scripts.Stages.FifthStage.Panels;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.FifthStage
{
    public class Port : MonoBehaviour , IPointerClickHandler
    {
        [SerializeField] private bool _isNotAddedToPortList;
        [SerializeField] private TMP_Text _namePort;
        [SerializeField] private TMP_Dropdown _portDropdown;
        [SerializeField] private GameObject _selectPanel;
        private PortListPanel _portListPanel;
        public string NamePortText => _namePort.text;
        public string PortType => _portDropdown.options[_portDropdown.value].text;

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
            _portListPanel.SelectPort(this);
        }

        private void Start()
        {
            _portListPanel = FindObjectOfType<PortListPanel>();
            if(!_isNotAddedToPortList)
                _portListPanel.AddPort(this);
        }

        public void SetValue(string namePort)
        {
            _namePort.text = namePort;
        }

        public void Select()
        {
            _selectPanel.SetActive(true);
        }

        public void UnSelect()
        {
            _selectPanel.SetActive(false);
        }


    }
}