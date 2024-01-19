using Assets.Scripts.Stages.SecondStage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutomaticLoadMenu : MonoBehaviour
{
    private void Start()
    {
        //if (PlayerPrefs.HasKey("LoadMenu"))
        //{
        //    FindObjectOfType<SecondStageController>().TryReportAllStages();
        //    PlayerPrefs.DeleteKey("LoadMenu");
        //    SceneManager.LoadScene("Menu");
        //}        
        Invoke(nameof(Load),0.3f);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("LoadMenu"))
        {
            FindObjectOfType<SecondStageController>().TryReportAllStages(true);
            ExamSystem.Instance.RegisterResult();
            PlayerPrefs.DeleteKey("LoadMenu");
            SceneManager.LoadScene("Menu");
        }
    }
}
