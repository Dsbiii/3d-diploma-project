using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Instruments
{
    public abstract class InstrumentService : MonoBehaviour
    {
        public abstract void Close();
        public abstract void Open();
        public abstract void ResetInstruemnt();
    }
}