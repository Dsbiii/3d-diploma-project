using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Users",menuName = "ScriptableObjects/Users")]
public class Users : ScriptableObject
{
    [SerializeField] private List<User> _users = new List<User>();
    public IEnumerable<User> UsersList => _users;

    public void AddUser(User user)
    {
        _users.Add(user);
    }
}

public class User
{
    [SerializeField] private int id;

    public int ID { get; private set; }
    public string LastFirstname { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }
    public string WorkName { get; private set; }
    public string DateTrening { get; private set; }
    public string DateExime { get; private set; }
    public string PointError { get; private set; }
    public int PointExime { get; private set; } 
    public bool RootPrava { get; private set; }
    public string DateCreate { get; private set; }

    private List<Exam> _exams = new List<Exam>();
    public void AddExam(Exam exam)
    {
        _exams.Add(exam);
    }
}
