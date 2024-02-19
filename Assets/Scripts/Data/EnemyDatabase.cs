using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using  System.Runtime.Serialization.Formatters.Binary;
[CreateAssetMenu(menuName = "Databases/Enemies", fileName = "Enemies")]
public class EnemyDatabase : ScriptableObject
{
    [SerializeField, HideInInspector] public List<EnemyData> enemyList;
    [SerializeField] public EnemyData currentEnemy;
    public int currentIndex = 0;


    public void Save()
    {
        Debug.Log("Save");
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.dataPath, "BD.Save"));
        bf.Serialize(file, saveData);
        file.Close();
    }
    
    public void Load()
    {
        Debug.Log("Application.dataPath " + Application.dataPath);
        if (File.Exists(string.Concat(Application.dataPath,"BD.Save")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.dataPath,"BD.Save"), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(),this);
            file.Close();
        }
    }
    
    
    public void AddElement()
    {
        if (enemyList==null)
        {
            enemyList = new List<EnemyData>();
        }
        currentEnemy = new EnemyData();
        enemyList.Add(currentEnemy);
        currentIndex = enemyList.Count - 1;
        currentEnemy.ID = currentIndex;
    }

    public void AddEnemyData(EnemyData enemyData)
    {
        if (enemyList == null)
        {
            enemyList = new List<EnemyData>();
        }
        currentEnemy = enemyData;
        enemyList.Add(currentEnemy);
        currentIndex = enemyList.Count - 1;
        currentEnemy.ID = currentIndex;
    }

    public void RemoveCurrentElement(EnemyData enemyData)
    {
        enemyList.Remove(enemyData);


        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].ID = i;
        }
    }

    public EnemyData GetNext()
    {
        if (currentIndex< enemyList.Count - 1)
        {
            currentIndex++;
        }

        currentEnemy = this[currentIndex];
        return currentEnemy;
    }
    
    public EnemyData GetPrev()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }

        currentEnemy = this[currentIndex];
        return currentEnemy;
    }

    public void ClearDatabase()
    {
        enemyList.Clear();
        enemyList.Add(new EnemyData());
        currentEnemy = enemyList[0];
        currentIndex = 0;
    }

    
    
    public EnemyData this[int index]
    {
        get
        {
            if (enemyList != null && index >= 0 && index < enemyList.Count)
            {
                return enemyList[index];
            }

            return null;
        }
        set
        {
            if (enemyList == null)
            {
                enemyList = new List<EnemyData>();
            }

            if (index>=0 && index < enemyList.Count && value != null)
            {
                enemyList[index] = value;
                
            }
            else
            {
                Debug.LogError("Выход за пределы массива, либо переданое значение = null");
            }
        }
    }
    
}
[System.Serializable]
public class EnemyData
{
    [SerializeField] private int id;


    public List<Exam> _exams = new List<Exam>();
    public void AddExam(Exam exam)
    {
        _exams.Add(exam);
        pointExime += exam.ScoreForExam;
        PointsWithCriticalErros += exam.ScoreForExamWithCriticalError;
    }

    public int PointsWithCriticalErros { get; set; }

    public IReadOnlyList<Exam> Exams => _exams;

    public int ID
    {
        get { return id; }
        set { id = value; }
    }
    
    [Tooltip("ФИО")] 
    [SerializeField] private string lastFirstname;

    public string LastFirstname
    {
        get { return lastFirstname; }
        set { lastFirstname = value; }
    }
    
    [Tooltip("Логин")] 
    [SerializeField] private string login;

    public string Login
    {
        get { return login; }
        set { login = value; }
    }
    
    [Tooltip("Пароль")] 
    [SerializeField] private string password;

    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    
    [Tooltip("Подразделение")] 
    [SerializeField] private string workName;

    public string WorkName
    {
        get { return workName; }
        set { workName = value; }
    }
    
    [Tooltip("Дата тренировки")] 
    [SerializeField] private string dateTrening;
    public string DateTrening
    {
        get { return dateTrening; }
        set { dateExime = value; }
    }
    
    [Tooltip("Дата Экзамена")] 
    [SerializeField] private string dateExime;
    public string DateExime
    {
        get { return dateExime; }
        set { dateExime = value; }
    }
    [Tooltip("Место ошибки")] 
    [SerializeField] private string pointError;
    public string PointError
    {
        get { return pointError; }
        set { pointError = value; }
    }
    [Tooltip("Кол-во Баллов")] 
    [SerializeField] private int pointExime;
    public int PointExime
    {
        get { return pointExime; }
        set { pointExime = value; }
    }
    [Tooltip("Права юзера")] 
    [SerializeField] private bool rootPrava;
    public bool RootPrava
    {
        get { return rootPrava; }
        set { rootPrava = value; }
    }
    
    [Tooltip("Дата созданмя")] 
    [SerializeField] private string dateCreate;
    public string DateCreate
    {
        get { return dateCreate; }
        set { dateCreate = value; }
    }
}


