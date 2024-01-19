using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Objects
{
    public class TSObject : MonoBehaviour
    {
        public bool IsPlanting { get; private set; }


        public void Plant()
        {
            IsPlanting = true;
        }
    }
}