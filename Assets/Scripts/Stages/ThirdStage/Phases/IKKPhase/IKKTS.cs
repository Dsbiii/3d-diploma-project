using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases.IKKPhase
{
    public class IKKTS : Item
    {
        public bool IsPlanted { get; private set; }

        public void Plant()
        {
            IsPlanted = true;
        }
    }
}