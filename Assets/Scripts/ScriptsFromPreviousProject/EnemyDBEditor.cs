using System;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

[CustomEditor(typeof(EnemyDatabase))]
public class EnemyDBEditor : Editor
{
    private EnemyDatabase database;

    private void Awake()
    {
        database = (EnemyDatabase) target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("RemoveAll"))
        {
            database.ClearDatabase();
        }
        if (GUILayout.Button("Remove"))
        {
            //database.RemoveCurrentElement();
        }
        if (GUILayout.Button("Add"))
        {
            database.AddElement();
        }
        if (GUILayout.Button("<="))
        {
            database.GetPrev();
        }
        if (GUILayout.Button("=>"))
        {
            database.GetNext();
        }
        
        
        
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
        
    }
}
#endif
