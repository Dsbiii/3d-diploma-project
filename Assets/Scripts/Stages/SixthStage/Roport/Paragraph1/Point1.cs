using Assets.Scripts.Stages.SixthStage.Directories;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage.Roport.Paragraph1
{
    public class Point1 : MonoBehaviour
    {
        [SerializeField] private DirectoriesPanel _objects;
        private void CheckReport()
        {
            List<EquipmentObjectType> equipments = _objects.EquipmentObjectTypes;

        }
    }
}