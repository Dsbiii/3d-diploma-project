using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class UsersAndRolesPanel : MonoBehaviour
    {
        [SerializeField] private List<SelectObject> _selectObjects;
        public bool GetUser(out string value)
        {
            foreach (var item in _selectObjects)
            {
                if (item.IsSelected)
                {
                    value = item.Value;
                    return true;
                }
            }
            value = "";
            return false;
        }
        public void SelectedAbonent(SelectObject selectObject)
        {
            foreach (var item in _selectObjects)
            {
                if(item != selectObject)
                {
                    item.Unselect();
                }
            }
        }
        public void AddAbonent(SelectObject abonent)
        {
            _selectObjects.Add(abonent);
        }
        public void RemoveAbonent(SelectObject abonent)
        {
            _selectObjects.Remove(abonent);
            Destroy(abonent.gameObject);
        }

    }
}