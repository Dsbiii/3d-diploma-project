using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class UsersAndRolesPanel : MonoBehaviour
    {
        [SerializeField] private List<SelectObject> _selectObjects;
        public int[] Report = new int[3] {0,0,0};
        private bool _passwordIsSetted;
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
        public void PasswordSet()
        {
            _passwordIsSetted = true;
            Report[2] = 1;
        }
        public int GetPoints()
        {
            if (_selectObjects.Count == 0)
                return 0;
            Report[0] = _selectObjects.OrderByDescending(obj => obj.Points()).First().Report[0];
            Report[1] = _selectObjects.OrderByDescending(obj => obj.Points()).First().Report[1];
            if (_passwordIsSetted)
                return _selectObjects.OrderByDescending(obj => obj.Points()).First().Points() + 1;
            return _selectObjects.OrderByDescending(obj => obj.Points()).First().Points();
        }
    }
}