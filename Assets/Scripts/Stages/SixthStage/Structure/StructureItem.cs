using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage.Structure
{
    public class StructureItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Dropdown _dropdown;

        public string Name => _name.text;
        public string Type => _dropdown.options[_dropdown.value].text;
    }
}