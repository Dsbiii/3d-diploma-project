using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Stages.SecondStage
{
    public class ActionPicker 
    {
        public bool TryPickAction(out Action action)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            foreach (var item in results)
            {
                if (item.gameObject.TryGetComponent(out action))
                {
                    return true;
                }
            }
            action = null;
            return false;

        }
    }
}