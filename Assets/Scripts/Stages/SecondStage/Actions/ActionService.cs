using Assets.Scripts.Stages.FirstStage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage
{
    public class ActionService : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private List<Action> _actions;
        [SerializeField] private Transform _necessaryActionParent;
        [SerializeField] private Transform _noNecessaryActionParent;


        private List<Action> _necessaryList = new List<Action>();
        private List<Action> _noNecessaryList = new List<Action>();

        private GameState _secondStageState;
        private ActionPicker _actionPicker;


        public void Init(GameState secondStageState, ActionPicker actionPicker)
        {
            _secondStageState = secondStageState;
            _actionPicker = actionPicker;
        }


        public void Update()
        {
            if(_secondStageState.CurrentState == State.Selecting_Actions)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_actionPicker.TryPickAction(out Action action))
                    {
                        if (action.ActionType == ActionTypes.Necessary)
                        {
                            action.transform.SetParent(_noNecessaryActionParent);
                            _noNecessaryList.Add(action);
                            _necessaryList.Remove(action);
                        }
                        else
                        {
                            action.transform.SetParent(_necessaryActionParent);
                            _necessaryList.Add(action);
                            _noNecessaryList.Remove(action);
                        }
                        action.TurnType();
                    }
                }
            }
        }

    }
}