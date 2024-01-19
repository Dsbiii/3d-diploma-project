using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class SATCrteatePanel : MonoBehaviour
    {
        [SerializeField] private GameObject _item;
        [SerializeField] private TMP_InputField _serialNumberInputField;
        [SerializeField] private TMP_Dropdown _placeSetupDropDown;
        [SerializeField] private TMP_Text _placeSetupText;
        [SerializeField] private TMP_Text _serialNumberText;


        public void Add()
        {
            _item.SetActive(true);
            _placeSetupText.text = _placeSetupDropDown.options[_placeSetupDropDown.value].text;
            _serialNumberText.text = _serialNumberInputField.text;
        }

    }
}