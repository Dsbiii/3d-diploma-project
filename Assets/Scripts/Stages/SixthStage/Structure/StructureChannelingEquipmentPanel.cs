using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage.Structure
{
    public class StructureChannelingEquipmentPanel : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _structurePuParent;
        [SerializeField] private StructureItem _structureItem;
        [SerializeField] private StructurePU _structurePU;

        public void Add()
        {
            StructureItem structureItem = Instantiate(_structureItem , _parent);
            StructurePU structurePU = Instantiate(_structurePU, _structurePuParent);
            structurePU.Init(structureItem);
        }
    }
}