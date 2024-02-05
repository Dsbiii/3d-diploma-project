using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class AccountingPointService : MonoBehaviour
    {
        [SerializeField] private List<AccountingPointObject> _accountingPointObjects;
        [SerializeField] private Button _edit;
        [SerializeField] private Button _delete;
        [SerializeField] private AccountingPointObject _prefab;
        [SerializeField] private Transform _parent;
        public void Spawn()
        {
            AccountingPointObject accountingPointObject = Instantiate(_prefab, _parent);
            accountingPointObject.Init(_edit, _delete, this);
            _accountingPointObjects.Add(accountingPointObject);
        }
        public void Selected(AccountingPointObject accountingPointObject)
        {
            foreach (var obj in _accountingPointObjects)
            {
                if(accountingPointObject != obj)
                {
                    obj.Unselect();
                }
            }
        }
        public void Delete(AccountingPointObject accountingPointObject)
        {
            Destroy(accountingPointObject.gameObject);
            _accountingPointObjects.Remove(accountingPointObject);
        }
        public int GetPoints2()
        {
            if (_accountingPointObjects.Count == 0)
                return 0;
            return _accountingPointObjects.OrderByDescending(obj => obj.Points()).First().Points();
        }
        public int GetPoints3()
        {
            if (_accountingPointObjects.Count == 0)
                return 0;
            return _accountingPointObjects.OrderByDescending(obj => obj.SchedulePoint()).First().SchedulePoint();
        }
    }
}