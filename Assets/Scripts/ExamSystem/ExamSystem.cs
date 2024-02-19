using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Dates;
using UnityEngine;

public class ExamSystem : MonoBehaviour
{
    [SerializeField] private EnemyDatabase _enemyDatabase;
    private EnemyData _currentUser;
    private List<Exam> _exams = new List<Exam>();
    public int Score { get; private set; }
    public static ExamSystem Instance { get; private set; }
    public bool IsOpenSession { get; private set; }
    public IEnumerable<Exam> Exams => _exams;

    public void Awake()
    {
        Instance = GetComponent<ExamSystem>();
        DontDestroyOnLoad(this);
    }

    public void TakeDate()
    {
    }

    public void OpenSession()
    {
        IsOpenSession = true;
    }

    public void RegisterExitDate()
    {

    }


    public void TrySaveSessionIfOpen()
    {
        if (IsOpenSession)
        {
            RegisterResult();
        }
    }

    public void RegisterResult()
    {
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null) 
        {
            timer.GetOutExitTime(out float minutes, out float seconds);
            _currentUser.DateCreate = string.Format("{0:00} : {1:00}", Mathf.Abs(minutes), Mathf.Abs(seconds - 1));
            Debug.Log("DateCreated " + string.Format("{0:00} : {1:00}", Mathf.Abs(minutes), Mathf.Abs(seconds - 1)));
        }

        _currentUser.DateTrening = FindObjectOfType<Timer>().Time;
        _enemyDatabase.AddEnemyData(_currentUser);
        AddTimeExamenie();
        foreach (var item in _exams)
            _enemyDatabase.currentEnemy.AddExam(item);
        _enemyDatabase.Save();
        Score = _exams.Sum(item => item.Scores);
        IsOpenSession = false;
    }

    public void AddTimeExamenie()
    {
        _currentUser.DateExime = DateTime.Now.ToString();
    }

    public void EnterUser(EnemyData user)
    {
        OpenSession();
        _currentUser = user;
    }

    public void AddExam(int wrong, int right, string idealAction, string userAction, string examName)
    {
        _exams.Add(new Exam(wrong, right, idealAction, userAction, examName));
    }

    public void AddExam(string result, string idealAction, string userAction, string examName)
    {
        _exams.Add(new Exam(result,  idealAction, userAction, examName));
    }

    public void AddExam(Exam exam)
    {
        _exams.Add(exam);
    }
}


[System.Serializable]
public class Exam
{
    public int Wrong;
    public int Right;
    public string IdealAction;
    public string UserAction;
    public string ExamName;
    public string Result = null;
    public string ExamType;
    public int ScoreForExam;
    public int Scores;
    public bool ExamHaveCriticalError;
    public Exam(string examType)
    {
        ExamType = examType;
    }
    public Exam(int wrong, int right, string idealAction, string userAction, string examName)
    {
        Wrong = wrong;
        Right = right;
        IdealAction = idealAction;
        UserAction = userAction;
        ExamName = examName;
        Scores = Right - Wrong;
        ScoreForExam = Scores;
    }

    public Exam(string result, string idealAction, string userAction, string examName)
    {
        Result = result;
        IdealAction = idealAction;
        UserAction = userAction;
        ExamName = examName;
        Scores = Right - Wrong;
    }
}