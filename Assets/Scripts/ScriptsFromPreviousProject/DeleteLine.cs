using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLine : MonoBehaviour
{
    public EnemyDatabase database;
    public GameObject Line;
    public EditorDataBase EDB;
    public GameObject EditorWin;
    public EnemyData CurrentEnemyData;
    
    public int Ind;


    public void Delete()
    {
        if (Line != null)
        {
            if (CurrentEnemyData.Login == "1111" && CurrentEnemyData.Password == "1111")
                return;
            database.currentIndex = Ind;
            Destroy(Line);
            database.RemoveCurrentElement(CurrentEnemyData);
            database.Save();
            transform.parent.GetComponent<TableAllUsers>().ClearTable();
            transform.parent.GetComponent<TableAllUsers>().OpenTable();  
        }
        

    }
    
    public void EditorLine()
    {
        transform.parent.gameObject.SetActive(false);
        EditorWin.SetActive(true);
        EDB.EditeUser(Ind);
        EDB.Edit = true;
    }
}
