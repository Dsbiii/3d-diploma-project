using Assets.Scripts.Data;
using System.Collections;
using UnityEngine;
using UnityEditor;


        public class UserData : MonoBehaviour
        {
            private PlayerProfile _playerProfile;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public string Name { get; private set; }
        public string OrgName { get; private set; }
        public string DzoOrgName { get; private set; }
        public string DzoPlaceName { get; private set; }
        public string FactoryName { get; private set; }
        public string Status { get; private set; }
        public string Date { get; private set; }

    public void Init(string name, string orgName, string dzoOrgName, string dzoPlaceName, string factoryName , string status , string date)
        {
            Name = name;
            OrgName = orgName;
            DzoOrgName = dzoOrgName;
            DzoPlaceName = dzoPlaceName;
            FactoryName = factoryName;
            Status = status;
            Date = date;
            Debug.Log("Date " + Date);
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

        public void SetPlayerProfile(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }
    }
