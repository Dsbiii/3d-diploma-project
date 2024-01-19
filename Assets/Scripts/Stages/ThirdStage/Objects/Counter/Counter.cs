using Assets.Scripts.Stages.ThirdStage.Phases.CablesPhase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Objects.Counter
{
    public class Counter : TSObject
    {
        [SerializeField] private List<CablePoint> _cablePoints;

        public IEnumerable<CablePoint> CablePoints => _cablePoints;

    }
}