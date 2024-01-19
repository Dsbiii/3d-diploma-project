using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage
{
    public class FifthStageModel : MonoBehaviour
    {
        public bool IsRightConnectedComputer {  get; private set; }


        public void SetConnectComputerStatus(bool isRightConnectedComputer)
        {
            IsRightConnectedComputer = isRightConnectedComputer;
        }

    }
}