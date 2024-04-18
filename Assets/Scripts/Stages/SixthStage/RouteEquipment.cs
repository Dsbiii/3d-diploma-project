using Assets.Scripts.Stages.SixthStage.Directories;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class RouteEquipment : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Image _image;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private TMP_Text _type;
        [SerializeField] private TMP_Text _priority;
        [SerializeField] private TMP_Text _port;
        private EquipmentObjectType _equipment;
        private string _ipAdress;
        private RouteEquimentPanel _panel;
        private Button _edit;
        private Button _delete;
        private DirectoriesPanel directoriesPanel;

        public bool IsSelected { get; private set; } = false;
        private void Awake()
        {
            _toggle.onValueChanged.AddListener(CheckToggle);
        }
        public void Init(string type, string priority, Button edit, Button delete, RouteEquimentPanel panel, EquipmentObjectType equipment, DirectoriesPanel directoriePanel)
        {
            _type.text = type;
            _priority.text = priority;
            _edit = edit;
            _delete = delete;
            _panel = panel;
            directoriesPanel = directoriePanel;
            _equipment = equipment;
            _edit.onClick.AddListener(Edit);
            _delete.onClick.AddListener(Delete);
        }
        public void SetValue(string port, string IpAdress)
        {
            _port.text = port;
            _ipAdress = IpAdress;
            Debug.Log("Points" + SumPoint());
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsSelected)
                Select();
            else
            {
                Unselect();
                _edit.interactable = false;
                _delete.interactable = false;
            }
        }

        public void Select()
        {
            _toggle.SetIsOnWithoutNotify(true);
            _edit.interactable = true;
            _delete.interactable = true;
            _image.color = _selectColor;
            IsSelected = true;
            _panel.Selected(this);
        }

        public void Unselect()
        {
            _toggle.SetIsOnWithoutNotify(false);
            _image.color = _baseColor;
            IsSelected = false;
        }
        private void CheckToggle(bool state)
        {
            if (state)
            {
                Select();
            }
            else
            {
                Unselect();
                _edit.interactable = false;
                _delete.interactable = false;
            }
        }
        private void Edit()
        {
            _panel.Edit(this, _port.text, _ipAdress);
        }
        private void Delete()
        {
            _panel.Delete(this);
        }
        public int SumPoint()
        {
            if (_equipment.DontSM || _equipment.CriticalError)
                return 0;
            int Point = 0;
            if (_equipment == null)
                Point = 0;
            else
            {
                Debug.Log(" dsa1");
                Point = directoriesPanel.Getpoints();
                for(int i = 0; i < _equipment.Report.Length; i++)
                {
                    _panel.ReportInfo[i] = _equipment.Report[i];
                }
            }
            if (CheckType())
            {
                _panel.ReportInfo[5] = 1;
                Point++;
            }
            if (CheckAdress())
            {
                _panel.ReportInfo[6] = 1;
                Point++;
            }
            if (CheckPort())
            {
                _panel.ReportInfo[7] = 1;
                Point++;
            }
            return Point;
        }
        private bool CheckType()
        {
            if(_type.text == "Исходящее TCР-соединение")
                return true;
            return false;
        }
        private bool CheckAdress()
        {
            if (_ipAdress == "10.169.35.32")
                return true;
            return false;
        }
        private bool CheckPort()
        {
            if (_port.text == "502")
                return true;
            return false;
        }
    }
}