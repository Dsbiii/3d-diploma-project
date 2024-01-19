using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.SecondStage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSessionButton : MonoBehaviour
{
    [SerializeField] private FirstStageController _firstStageController;

    public void TrySaveSession()
    {
        if (SceneManager.GetActiveScene().name == "FirstStage")
        {
            _firstStageController.AddScores();
        }
        if (SceneManager.GetActiveScene().name == "SecondStage")
        {
            FindObjectOfType<SecondStageController>().TryReportAllStages();
            ExamSystem.Instance.RegisterResult();
        }

    }
}
