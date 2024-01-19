using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckUser : MonoBehaviour
{
    [SerializeField]
    public EnemyDatabase database;

    public InputField _Login,_Password;

    public GameObject Adminka;

    [SerializeField] private MenuDataBase MB;


    public void Check()
    {
        if (_Login.text != "" && _Password.text != "")
        {
            for (int i = 0; i < database.enemyList.Count; i++)
            {

                if (_Login.text == database[i].Login && _Password.text == database[i].Password && database[i].RootPrava == true)
                {
                    Adminka.SetActive(true);
                    gameObject.SetActive(false);

                }
                 if (_Login.text == database[i].Login && _Password.text == database[i].Password && database[i].RootPrava == false)
                 {
                 
                     gameObject.SetActive(false);
                 }
                    
            }

        }
        ResetFieldValue();
    }

    public void ResetFieldValue()
    {
        _Login.text = "";
        _Password.text = "";
    }
        
}

   