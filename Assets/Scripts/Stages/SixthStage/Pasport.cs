using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class Pasport : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _type;
        [SerializeField] private TMP_Dropdown _abonent;
        [SerializeField] private TMP_Dropdown _abonent2;
        [SerializeField] private AbonentPanel _abonentPanel;
        private void OnEnable()
        {
            AddNewOptions(_abonentPanel.GetText(), _abonent);
            AddNewOptions(_abonentPanel.GetText(), _abonent2);
            _abonent.RefreshShownValue();
            _abonent2.RefreshShownValue();
        }
        public void RemoveItemByIndex(int indexToRemove, TMP_Dropdown dropdown)
        {
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>(dropdown.options);

            if (indexToRemove >= 0 && indexToRemove < options.Count)
            {
                options.RemoveAt(indexToRemove);
                _abonent.ClearOptions();
                _abonent.AddOptions(options);
            }
            else
            {
                Debug.LogWarning("Invalid index to remove: " + indexToRemove);
            }
        }
        public void AddNewOptions(List<string> newOptions, TMP_Dropdown dropdown)
        {
            List<TMP_Dropdown.OptionData> newDropdownOptions = new List<TMP_Dropdown.OptionData>();
            foreach (string option in newOptions)
            {
                newDropdownOptions.Add(new TMP_Dropdown.OptionData(option));
            }

            dropdown.AddOptions(newDropdownOptions);
        }
        private void OnDisable()
        {
            for(int i = _abonent.options.Count;  i > 2; i--) 
            {
                RemoveItemByIndex(i, _abonent);
                RemoveItemByIndex(i, _abonent2);
            }
        }
        public int Points()
        {
            int Point = 0;
            if (_type.value == 2)
                Point++;
            if (_abonent.value > 2)
                Point++;
            return Point;
        }
    }
}