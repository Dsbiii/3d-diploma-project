using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage.Structure
{
    public class StructurePU : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _type;

        private StructureItem _structureItem;

        public void Init(StructureItem item)
        {
            _structureItem = item;
            _name.text = item.Name;
            _type.text = item.Type;
        }

        private void OnEnable()
        {
            _name.text = _structureItem.Name;
            _type.text = _structureItem.Type;
        }
    }
}