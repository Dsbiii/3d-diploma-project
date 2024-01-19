using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases
{
    public abstract class Phase : MonoBehaviour
    {
        public abstract void EnterInPhase();
        public abstract void ExitFromPhase();
    }
}