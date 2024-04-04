using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameMode : MonoBehaviour
    {
        public bool IsDemo;
        public void SetupDemo(bool isDemo)
        {
            IsDemo = isDemo;
        }
    }
}