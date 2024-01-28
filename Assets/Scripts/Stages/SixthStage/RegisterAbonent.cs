using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private List<string> _text;
        public int TextCount => _text.Count;



        public void Sumbit()
        {
            _text.Add(_surname.text + " " + _name.text + " " + _lastName.text + ", " + _serialNumber.text);
        }
        public string GetText(int index)
        {
            return _text[index];
        }
        
    }
}