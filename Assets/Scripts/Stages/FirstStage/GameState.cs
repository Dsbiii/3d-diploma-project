using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FirstStage
{
    public enum State
    {
        Selecting_Item,
        Preview,
        Inventory,
        Selecting_Actions,
        Inspection,
        Dismantling,
        CE602M,
        PlantTSObjects,
        SelectSTObject,

    }

    public class GameState : MonoBehaviour
    {

        [SerializeField] private State _state = State.Selecting_Item;
        public State CurrentState => _state;


        public void EnterInCE602M()
        {
            _state = State.CE602M;
        }
        public void EnterInSelectTCObjectState()
        {
            _state = State.SelectSTObject;
        }

        public void EnterInPlantTSObjectsState()
        {
            _state = State.PlantTSObjects;
        }
        public void EnterInSelectingActions()
        {
            _state = State.Selecting_Actions;
        }

        public void EnterInDismantling()
        {
            _state = State.Dismantling;
        }

        public void EnterInInspection()
        {
            _state = State.Inspection;
        }

        public void EnterInSelectingItemState()
        {
            _state = State.Selecting_Item;
        }

        public void EnterInInventoryState()
        {
            _state = State.Inventory;
        }

        public void EnterInPreviewState()
        {
            _state = State.Preview;
        }


    }
}