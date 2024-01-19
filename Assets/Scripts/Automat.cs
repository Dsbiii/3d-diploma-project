using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automat : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject[] _levers;

    private void Update()
    {
        if (_target.activeSelf)
        {
            foreach(var item in _levers)
            {
                item.transform.eulerAngles = new Vector3 (0, 180, 58.23f);
            }
        }
        else
        {
            foreach (var item in _levers)
            {
                item.transform.eulerAngles = new Vector3(0, 180, -58.23f);
            }
        }
    }

}
