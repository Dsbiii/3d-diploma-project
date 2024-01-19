using Assets.Scripts.Stages.SixStage;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage
{
    public class ContextPanelSixStage : MonoBehaviour
    {
        private CreateContextMenuSixStage _createContextMenuSixStage;


        public void SetCreateContextMenuSixStage(CreateContextMenuSixStage createContextMenuSixStage)
        {
            _createContextMenuSixStage = createContextMenuSixStage;
        }

        public void OpenRoute()
        {
            _createContextMenuSixStage.OpenRoute();
        }

        public void OpenDiganosticRoute()
        {
            _createContextMenuSixStage.OpenDiagnosticRoute();
        }


    }
}