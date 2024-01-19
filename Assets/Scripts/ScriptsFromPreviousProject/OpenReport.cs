using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenReport : MonoBehaviour
{
    private TableReportExime _tableReportExime;
    public int Index;
    public GameObject TableExime;

    private void Awake()
    {
        _tableReportExime = FindObjectOfType<TableReportExime>();
    }

    private void Update()
    {
        TableExime = GameObject.Find("TableAllUser").GetComponent<TableAllUsers>().TableExime;
    }


    public void OpenTable()
    {
        TableExime.SetActive(true);
        _tableReportExime.Ind = Index;
        _tableReportExime.OpenTable();
    }
}
