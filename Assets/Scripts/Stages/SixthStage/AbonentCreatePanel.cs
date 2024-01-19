using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class AbonentCreatePanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _loginValue;
        [SerializeField] private TMP_Text _abonentValue;
        [SerializeField] private TMP_Text _abonentProfileValue;
        [SerializeField] private Transform _parent;
        [SerializeField] private SelectObject _prefab;
        [SerializeField] private UsersAndRolesPanel _usersAndRolesPanel;
        [SerializeField] private Button _edit;
        [SerializeField] private Button _delete;
        [SerializeField] private GameObject _panel;
        private Proccess proccess = Proccess.Add;
        private SelectObject _currentAbonent; 
        public void OkButtonClick()
        {
            Debug.Log("CheckWork");
            if(proccess == Proccess.Add)
            {
                Debug.Log("ItsWork");
                Spawn();
            }
            else
            {

                Debug.Log("ItsDoesntWork");
                _currentAbonent.SetValue(_loginValue.text, _abonentValue.text, _abonentProfileValue.text);
                proccess = Proccess.Add;
            }
        }
        public void Spawn()
        {
            SelectObject selectObject = Instantiate(_prefab, _parent);
            selectObject.Init(_loginValue.text, _abonentValue.text, _abonentProfileValue.text, _edit, _delete, this);
            _usersAndRolesPanel.AddAbonent(selectObject);
        }
        public void Edit(string login, string abonent, string abonentProfile)
        {
            proccess = Proccess.Edit;
            _loginValue.text = login;
            _abonentValue.text = abonent;
            _abonentProfileValue.text = abonentProfile;
            _panel.SetActive(true);
        }
        public void Select(SelectObject selectObject)
        {
            _usersAndRolesPanel.SelectedAbonent(selectObject);
        }
        public void Clean() 
        {
            _loginValue.text = "";
            _abonentProfileValue.text = "Выбрать...";
            _abonentValue.text = "Выбрать...";
        }
        public void Delete(SelectObject selectObject)
        {
            _usersAndRolesPanel.RemoveAbonent(selectObject);
        }
    }
}