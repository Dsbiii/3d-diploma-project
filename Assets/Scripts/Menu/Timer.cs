using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Stages.FirstStage;
using Assets.Scripts.Stages.SecondStage;
using Assets.Scripts.Stages.FifthStage;
using Assets.Scripts.Stages.FourthStage;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _score;
    [SerializeField] private Text _timeRemining;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TMP_Text _baseTimerText;
    [SerializeField] private TMP_Text _smTimerText;
    [SerializeField] private TMP_Text _pyramidTimerText;
    [SerializeField] private Setting _setting;
    [SerializeField] private GameObject _pyramidGameObject;
    [SerializeField] private GameObject _smGameObject;

    private TMP_Text timerText;
    public float _timeLeft = 0f;
    private float _startTime;
    private float _baseTime;
    private Coroutine _timer;
    private bool _isEndTime;
    private bool _startedTime;

    public string Time => timerText.text;

    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= UnityEngine.Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
    }

    public void StartTime()
    {
        timerText = _baseTimerText;
        _timeLeft = _setting._TimeRun * 60;
        _startTime = _setting._TimeRun * 60;
        _baseTime = _setting._TimeRun;
        _startedTime = true;
        _timer = StartCoroutine(StartTimer());
    }

    private void Update()
    {
        if (_pyramidGameObject.activeSelf)
        {
            _smTimerText.gameObject.SetActive(false);
            timerText = _pyramidTimerText;
        }
        if (_smGameObject.activeSelf && !_pyramidGameObject.activeSelf)
        {
            timerText = _smTimerText;
            _smTimerText.gameObject.SetActive(true);
        }
        if(!_pyramidGameObject.activeSelf && !_smGameObject.activeSelf)
        {
            _smTimerText.gameObject.SetActive(false);
            timerText = _baseTimerText;
        }

        if(_timeLeft == 0 && !_isEndTime && _startedTime)
        {
            _isEndTime = true;
            if (SceneManager.GetActiveScene().name == "FirstStage")
                FindObjectOfType<FirstStageController>().AddScores();

            FindObjectOfType<FourthStageExamSystem>().RegisterExamSystem();
            FindObjectOfType<FifthStageExam>().RegisterFifthStageExam();
            ExamSystem.Instance.RegisterResult();

            GetOutReminingTime(out float minutes, out float seconds);
            StopTimer();
            _timeRemining.text = "¬–≈Ãﬂ Œ—“¿ÀŒ—‹: " + string.Format("{0:00} : {1:00}", Mathf.Abs(minutes), Mathf.Abs(seconds - 1));
            _resultPanel.SetActive(true);
            _score.text = "¡¿ÀÀ€: " + ExamSystem.Instance.ScoreWithCrititalErrors;
        }

    }



    public void GetOutReminingTime(out float minutes, out float seconds)
    {
        float time = _baseTime - _timeLeft;
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);
    }

    public void GetOutExitTime(out float minutes, out float seconds)
    {
        float time = _startTime - _timeLeft;
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);
    }

    public string GetStringExitTime()
    {
        return FormatTime(_startTime - _timeLeft);
    }

    string FormatTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        int remainingSeconds = Mathf.FloorToInt(seconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes, remainingSeconds);
    }

    public void StopTimer()
    {
        if(_timer != null)
            StopCoroutine(_timer);
        timerText.text = "";
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

    }

}
