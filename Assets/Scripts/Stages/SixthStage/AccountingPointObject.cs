using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class AccountingPointObject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Color _selectColor;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _editPanel;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _date;
        [SerializeField] private TMP_Text _period;
        [SerializeField] private TMP_InputField _start;
        public bool IsSelect;
        private Button _edit;
        private Button _delete;
        private AccountingPointService _service;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_panel.activeSelf)
            {
                if (!IsSelect)
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
        }
        public void Init(Button edit, Button delete, AccountingPointService service)
        {
            _edit = edit;
            _delete = delete;
            _service = service;
            _edit.onClick.AddListener(Edit);
            _delete.onClick.AddListener(Delete);
        }
        private void Select()
        {
            _edit.interactable = true;
            _delete.interactable = true;
            IsSelect = true;
            _image.color = _selectColor;
            _service.Selected(this);
        }
        public void Unselect()
        {
            IsSelect = false;
            _image.color = _baseColor;
        }
        private void Edit()
        {
            if (IsSelect)
            {
                _editPanel.SetActive(true);
                _panel.SetActive(false);
            }

        }
        private void Delete()
        {
            if (IsSelect)
                _service.Delete(this);
        }
        public int Points()
        {
            int Points = 0;
            if (_name.text == "СЭТ-4ТМ 0.3М")
                Points++;
            if (_date.text != "")
                Points++;
            return Points;
        }
        public int SchedulePoint()
        {
            int Points = 1;
            if (_name.text != "")
                Points++;
            if (_start.text != "")
                Points += 2;
            if (_period)
                Points += 2;
            return Points;

        }
    }
}