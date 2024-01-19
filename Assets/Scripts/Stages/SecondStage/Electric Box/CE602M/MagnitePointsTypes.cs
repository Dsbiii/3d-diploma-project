using System;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M
{
    public enum MagnitePointsTypes
    {
        Pliers,
        Red_Crocodile_Clip,
        Green_Crocodile_Clip,
        Yellow_Crocodile_Clip,
        Black_Crocodile_Clip,
        Probes,
        Binocle
    }

    [Serializable]
    public class Type
    {
        [SerializeField] private MagnitePointsTypes _magnitePointsTypes;
        [SerializeField] private GameObject _object;

        public GameObject Object => _object;
        public MagnitePointsTypes MagnitePointsTypes => _magnitePointsTypes;
    }
}