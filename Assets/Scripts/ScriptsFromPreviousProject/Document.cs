using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : MonoBehaviour
{
   public GameObject _Panel;
   public GameObject[] _Doc;

   public void Open()
   {
        Debug.Log("Open");
      if (_Panel.activeSelf)
      {
         _Panel.SetActive(false);
      }
      else
      {
         _Panel.SetActive(true);
      }
   }

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape) && _Panel.activeSelf)
      {
         for (int i = 0; i < _Doc.Length; i++)
         {
            _Doc[i].SetActive(false);
         }
         _Panel.SetActive(false);
      }

   }
}
