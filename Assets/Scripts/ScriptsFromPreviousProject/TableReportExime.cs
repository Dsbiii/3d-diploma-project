using Assets.Scripts.AdminPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableReportExime : MonoBehaviour
{
    public EnemyDatabase database;
    public DividingSlot dividingSlot;
    public int Ind;
    public ExamenieUserItem ExamenieUserItem;
    public Transform Parret;
    private List<GameObject> _items = new List<GameObject>();


    public void OpenTable()
    {

        int ind = 0;
        int slotCount = 0;
        for (int i = 0; i < database[Ind].Exams.Count; i++)
        {
            ind += 1;
     
            if (database[Ind].Exams[i].ExamType != null && database[Ind].Exams[i].ExamType != "" && database[Ind].Exams[i].ExamType.Length > 0)
            {
                int score = 0;
                for (int k = i + 1; k < database[Ind].Exams.Count; k++)
                {
                    if (database[Ind].Exams[k].ExamType != null && database[Ind].Exams[k].ExamType != "" && database[Ind].Exams[k].ExamType.Length > 0)
                    {
                        break;
                    }
                    score += database[Ind].Exams[k].ScoreForExam;

                }
                if (database[Ind].Exams[i].ExamHaveCriticalError)
                    score = 0;
                var item = Instantiate(dividingSlot, Parret);
                item.SetText(database[Ind].Exams[i].ExamType, score);
                _items.Add(item.gameObject);
            }
            else
            {
                if(!database[Ind].Exams[i].IsAdditionExam)
                    slotCount++;
                var item = Instantiate(ExamenieUserItem, Parret);
                Debug.Log("database[Ind].Exams[i].IsAdditionExam " + database[Ind].Exams[i].IsAdditionExam);
                if (database[Ind].Exams[i].IsAdditionExam)
                {
                    item.GetComponent<Image>().color = new Color(0.79f, 0.79f, 0.79f);
                }
                if (database[Ind].Exams[i].Result != null && database[Ind].Exams[i].Result.Length > 0)
                {
                    item.SetExamenieItems(slotCount.ToString(), database[Ind].Exams[i].ExamName, database[Ind].Exams[i].UserAction, database[Ind].Exams[i].IdealAction, database[Ind].Exams[i].Result);
                }
                else if (database[Ind].Exams[i].IsAdditionExam)
                {
                    item.SetExamenieItems("", database[Ind].Exams[i].ExamName, database[Ind].Exams[i].UserAction, database[Ind].Exams[i].IdealAction, "");
                }
                else
                {
                    item.SetExamenieItems(slotCount.ToString(), database[Ind].Exams[i].ExamName, database[Ind].Exams[i].UserAction, database[Ind].Exams[i].IdealAction, database[Ind].Exams[i].Scores.ToString());
                }
                _items.Add(item.gameObject);
            }
        }

    }

    public void ClearItems()
    {
        foreach (var item in _items)
            Destroy(item);
        _items.Clear();
    }

}




    
    

