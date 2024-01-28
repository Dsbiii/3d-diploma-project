using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class RegisterAbonentService : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _name;
        [SerializeField] private TMP_InputField _surname;
        [SerializeField] private TMP_InputField _lastName;
        [SerializeField] private TMP_InputField _serialNumber;
        [SerializeField] private GameObject _abonent;
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private TMP_Dropdown _dropdown2;
        [SerializeField] private TMP_Text _abonentPanel;
    }
}