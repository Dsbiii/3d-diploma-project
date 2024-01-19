using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AdminPanel
{
    public class UserItem : MonoBehaviour
    {
        [SerializeField] private SelectLine _selectLine;
        [SerializeField] private GameObject _isRootIcon;
        [SerializeField] private Text _fioText;
        [SerializeField] private Text _login;
        [SerializeField] private Text _password;
        [SerializeField] private Text _nameWork;
        [SerializeField] private Text _dateCreate;
        [SerializeField] private Text _lastExamenie;
        [SerializeField] private Text _report;

        private TableAllUsers _tableAllUsers;

        private void Awake()
        {
            _tableAllUsers = FindObjectOfType<TableAllUsers>();
        }

        public void SetText(EnemyData enemyData,string fio, string login , string password, string nameWork, string dateCreate , string lastExamenie, string report, bool isRoot)
        {
            _selectLine.EnemyData = enemyData;
            _fioText.text = fio;
            _login.text = login;
            _password.text = password;
            _nameWork.text = nameWork;
            _dateCreate.text = dateCreate;
            _lastExamenie.text = lastExamenie;
            _report.text = report;
            _isRootIcon.SetActive(isRoot);
        }

    }
}