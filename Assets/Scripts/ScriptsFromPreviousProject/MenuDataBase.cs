using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;



public class MenuDataBase : MonoBehaviour
{
    [SerializeField]
    public EnemyDatabase database;
    public int IndUser;
    public Text Name;
    public bool Exime = false;
    public float Time;
    public bool First;


    private static MenuDataBase _instance;

    public void OnEnable()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Awake()
    {
        if (_instance) DestroyImmediate(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Save");
            database.Save();
        }
    }

    private void Start()
    {
        database.Load();
    }


    public void TextName()
    {
        Name.text = database[IndUser].LastFirstname;
    }
}




