using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class ContextPanel : MonoBehaviour
    {
        private CreateContextMenu _createContext; 

        public void SetCreateContextMenu(CreateContextMenu createContextMenu)
        {
            _createContext = createContextMenu;
        }

        public void AddPort()
        {
            _createContext.AddPort();
        }

        public void Remove()
        {
            _createContext.Remove();
        }
    }
}