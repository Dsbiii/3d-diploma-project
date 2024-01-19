using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLine : MonoBehaviour
{
    public int Index;
    private bool Sel;
    public EnemyData EnemyData;


    public void ResetLine()
    {
        
    }

    public void Select()
    {

        if (Sel)
        {
            Sel = false;
            GetComponent<Image>().color = Color.white;
            GameObject.Find("Down").GetComponent<DeleteLine>().Line = null;
        }
        else
        {
            if (GameObject.Find("Down").GetComponent<DeleteLine>().Line != null)
            {
                var selectLine = GameObject.Find("Down").GetComponent<DeleteLine>().Line.GetComponent<SelectLine>();
                selectLine.Sel = false;
                selectLine.GetComponent<Image>().color = Color.white;
            }
            Sel = true;
            GetComponent<Image>().color = Color.cyan;
            GameObject.Find("Down").GetComponent<DeleteLine>().Line = gameObject;
            GameObject.Find("Down").GetComponent<DeleteLine>().CurrentEnemyData = EnemyData;
        }


        //if (Sel == false &&  GameObject.Find("Down").GetComponent<DeleteLine>().Line == null)
        //{
        //    Sel = true;
        //    GetComponent<Image>().color = Color.cyan; 
        //    GameObject.Find("Down").GetComponent<DeleteLine>().Line = gameObject;
        //    GameObject.Find("Down").GetComponent<DeleteLine>().CurrentEnemyData = EnemyData;
        //}
        //else if (Sel == true)
        //{
        //    Sel = false;
        //    GetComponent<Image>().color = Color.white;
        //    GameObject.Find("Down").GetComponent<DeleteLine>().Line = null;
        //}

        GameObject.Find("Down").GetComponent<DeleteLine>().Ind = Index;
        
    }
}
