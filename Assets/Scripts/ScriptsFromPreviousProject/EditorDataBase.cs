using UnityEngine;
using UnityEngine.UI;

public class EditorDataBase : MonoBehaviour
{
    [SerializeField]
    public EnemyDatabase database;

    public GameObject ErrorEdit;
    public GameObject ErrorNull;
    private bool Error;
    public GameObject GoodWin;
    public InputField _FIO;
    public InputField _Login;
    public InputField _Password;
    public bool Edit;

    private int Ind;
    public bool coincidence;

    public void AddUser()
    {
        
        foreach (var VARIABLE in database.enemyList)
        {
            if (VARIABLE.Login == _Login.text)
            {
                coincidence = true;
            }
            
        }

        if (_Login.text != "" && _Password.text != "")
        {

            if (coincidence == false)
            {
                database.AddElement();
                database.currentEnemy.LastFirstname = _FIO.text;
                database.currentEnemy.Login = _Login.text;
                database.currentEnemy.Password = _Password.text;
                database.currentEnemy.RootPrava = true;
                //database.currentEnemy.DateCreate = System.DateTime.Now.ToString();

                _FIO.text = "";
                _Login.text = "";
                _Password.text = "";
                GoodWin.SetActive(true);
                database.Save();
                coincidence = false;
            }
            else
            {
                coincidence = false;
                ErrorEdit.SetActive(true);
            }
        }
        else
        {
            coincidence = false;
            ErrorNull.SetActive(true);
        }
        //if (Edit == false)
        //{
        //    if (_Login.text != "" && _Password.text != "")
        //    {                
        //        if (coincidence == false)
        //        {
        //            database.AddElement();
        //            database.currentEnemy.LastFirstname = _FIO.text;
        //            database.currentEnemy.Login = _Login.text;
        //            database.currentEnemy.Password = _Password.text;
        //            database.currentEnemy.RootPrava = true;
        //            database.currentEnemy.DateCreate = System.DateTime.Now.ToString();

        //            _FIO.text = "";
        //            _Login.text= "";
        //            _Password.text= "";
        //            GoodWin.SetActive(true);
        //            database.Save();
        //        }
        //        else
        //        {
        //            ErrorEdit.SetActive(true);
        //        }                                                                       
        //    }
        //    else
        //    {
        //        ErrorNull.SetActive(true);
        //    }                             
        //}
        //else
        //{
        //    if (_Login.text != "" && _Password.text != "")
        //    {
        //        if (coincidence == false || database.enemyList[Ind].Login == _Login.text)
        //        {
        //            database.enemyList[Ind].LastFirstname = _FIO.text;
        //            database.enemyList[Ind].Login = _Login.text;
        //            database.enemyList[Ind].Password = _Password.text;
        //            database.enemyList[Ind].RootPrava = true;
        //            _FIO.text = "";
        //            _Login.text = "";
        //            _Password.text = "";
        //            database.Save();
        //            Edit = false;
        //        }
        //        else
        //        {
        //            ErrorEdit.SetActive(true);
        //        }
        //    }
        //}               
    }

    public void EditeUser(int index)
    {
        _FIO.text = database.enemyList[index].LastFirstname;
        _Login.text = database.enemyList[index].Login;
        _Password.text = database.enemyList[index].Password;
        Ind = index;
    }


    public void Exit()
    {
        _FIO.text = "";
        _Login.text= "";
        _Password.text= "";
        Edit = false;
    }
    
    public void ErrorExit()
    {
        coincidence = false;
    }
}
