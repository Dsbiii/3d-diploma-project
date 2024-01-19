using Assets.Scripts.Stages.ThirdStage.Objects.Counter;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases
{
    public interface ITransferHandler
    {
        public bool IsPlaced();
        public void EndTransferItem(Item item);

    }
}