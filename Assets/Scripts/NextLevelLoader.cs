using Assets.Scripts.Stages.SecondStage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{
    [SerializeField] private string _scene;
    [SerializeField] private bool _loadMenu;

    public void Load()
    {
        if (_loadMenu && SceneManager.GetActiveScene().name == "FirstStage")
        {
            Debug.Log("Loading from menu");
            PlayerPrefs.SetInt("LoadMenu", 1);
            PlayerPrefs.SetString("PreviosStage", "SecondStage");
            SceneManager.LoadScene("SecondStage");
        }
        //else if(_loadMenu && SceneManager.GetActiveScene().name == "SecondStage" && FindObjectOfType<SecondStageController>().IsAddmissionStage)
        //{
        //    PlayerPrefs.SetInt("LoadMenu", 1);
        //    PlayerPrefs.SetString("PreviosStage", "SecondStage");
        //    SceneManager.LoadScene("SecondStage");
        //}
        else
        {
            PlayerPrefs.SetString("PreviosStage", _scene);
            SceneManager.LoadScene(_scene);
        }
    }

}
