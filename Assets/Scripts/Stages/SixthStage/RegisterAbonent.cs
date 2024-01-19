using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class RegisterAbonent : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _name;
        [SerializeField] private TMP_InputField _surname;
        [SerializeField] private TMP_InputField _lastName;
        [SerializeField] private TMP_InputField _serialNumber;
        [SerializeField] private GameObject _abonent;
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private TMP_Dropdown _dropdown2;
        [SerializeField] private TMP_Text _abonentPanel;


        public void Sumbit()
        {
            _abonent.gameObject.SetActive(true);
            Debug.Log(_surname.text + " " + _name.text + " " + _lastName.text + ", " + _serialNumber.text);
            _abonentPanel.text = _surname.text + " " + _name.text + " " + _lastName.text + ", " + _serialNumber.text;
            _dropdown.AddOptions(new System.Collections.Generic.List<TMP_Dropdown.OptionData>
            { new TMP_Dropdown.OptionData(_surname.text + " " + _name.text + " " + _lastName.text + ", " + _serialNumber.text) });
            _dropdown2.AddOptions(new System.Collections.Generic.List<TMP_Dropdown.OptionData>
            { new TMP_Dropdown.OptionData(_surname.text + " " + _name.text + " " + _lastName.text + ", " + _serialNumber.text) });
        }
        
        
    }
}