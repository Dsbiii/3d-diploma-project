using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.ThirdStage.Objects.Counter
{
    public class Transformer : Item
    {

        private Vector3 _startPosition;
        private Quaternion _startRotation;
        public bool IsPlant { get; private set; }

        private void Awake()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation; 
        }
        
        public void Plant()
        {
            IsPlant = true;
        }

        public void UnPlant()
        {
            IsPlant = false;
        }

        public void ResetPosition()
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }
    }
}