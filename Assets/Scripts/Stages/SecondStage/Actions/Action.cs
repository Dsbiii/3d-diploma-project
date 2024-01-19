using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SecondStage
{ 
    public class Action : MonoBehaviour
    {
        private Image _image;
        private Shadow _shadow;
        [SerializeField] private Color _colorNecessary;
        [SerializeField] private Color _colorNotNecessary;
        [SerializeField] private ActionTypes _actionType = ActionTypes.Necessary;
        [SerializeField] private string _action;

        public ActionTypes ActionType => _actionType;

        private void Awake()
        {
            _shadow = GetComponent<Shadow>();
            _image = GetComponent<Image>();
        }

        public void TurnType()
        {
            if (_actionType == ActionTypes.Necessary)
            {
                _actionType = ActionTypes.Not_Necessary;
                _image.color = _colorNotNecessary;

            }
            else
            {
                _actionType = ActionTypes.Necessary;
                _image.color = _colorNecessary;
            }
        }

    }
}