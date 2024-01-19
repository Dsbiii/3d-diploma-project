using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    private MenuDataBase MenuDataBase;
    public GameObject[] _Menu;
    
    private void Start()
    {
        MenuDataBase = GameObject.Find("ControlManager").GetComponent<MenuDataBase>();
        if (MenuDataBase.First == false)
        {
            _Menu[0].SetActive(false);
            for (int i = 1; i < _Menu.Length; i++)
            {
                _Menu[i].SetActive(true);
            }
            
        }
    }

    public void StartTrening(string Scene)
    {
        MenuDataBase.First = false;
        SceneManager.LoadScene(Scene);
        
        
    }
    
    
    public void ExitPril()
    {
        Application.Quit();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
