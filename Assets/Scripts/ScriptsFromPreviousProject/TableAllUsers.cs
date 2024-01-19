using Assets.Scripts.AdminPanel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableAllUsers : MonoBehaviour
{
    public EnemyDatabase database;

    public UserItem _userItem;
    public GameObject TableContent;
    public GameObject TableExime;
    public GameObject TableChild;
    private List<UserItem> _userItems = new List<UserItem>();




    public void OpenTable()
    {
        for (int i = 0; i < database.enemyList.Count; i++)
        {
            var userItem = Instantiate(_userItem, TableContent.transform);
            if (database[i].RootPrava == true)
            {
                userItem.SetText(database[i],database[i].LastFirstname, database[i].Login, database[i].Password, database[i].WorkName, database[i].DateCreate, database[i].DateExime, database[i].PointExime.ToString(), true);
            }
            else
            {
                userItem.SetText(database[i], database[i].LastFirstname, database[i].Login, database[i].Password, database[i].WorkName, database[i].DateCreate, database[i].DateExime, database[i].PointExime.ToString(), false);
            }
            userItem.GetComponentInChildren<OpenReport>().Index = i;
            _userItems.Add(userItem);
        }
    }


    public void OpenExime()
    {
        TableExime.SetActive(true);
    }

    public void ClearTable()
    {
        foreach (var item in _userItems)
            Destroy(item.gameObject);

        _userItems.Clear();
    }

    public void ClearChildren()
    {
        int i = 0;

        
        GameObject[] allChildren = new GameObject[TableChild.transform.childCount];

       
        foreach (Transform child in TableChild.transform)
        {
            if (child.name != "GameObject")
            {
                allChildren[i] = child.gameObject;
                i += 1;
            }
            
        }
        
        foreach (GameObject child in allChildren)
        {
            Destroy(child);
        }

        
    }
}
