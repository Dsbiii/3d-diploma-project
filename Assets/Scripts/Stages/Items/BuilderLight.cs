using Assets.Scripts.Stages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderLight : Item
{
    public override void ActionInPreview()
    {
        if (transform.GetChild(0).gameObject.activeSelf == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        AddAction("-включить/выключить");
    }
}
