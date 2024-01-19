using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParmaElements : MonoBehaviour
{
    [SerializeField] private MeteringParma _meteringParma;
    [SerializeField] private Transform _child;
    [SerializeField] private ParmaComponent[] _parmaCamera;
    [SerializeField] private ParmaComponent[] _parmaList;

    public void ResetPositionsButton()
    {
        _meteringParma.ResetParma();
        foreach (var param in _parmaCamera)
        {
            foreach (var item in _parmaList)
            {
                if (item.gameObject.activeSelf)
                {
                    if (item._ItemType == param._ItemType)
                    {
                        param.ResetPosition();
                        item.ResetParma();
                    }
                }
            }
        }
    }

    public void OffImageParma()
    {
        GameObject.Find("Canvas").transform.Find("ImageParma").gameObject.SetActive(false);
    }

    public void ResetParmas()
    {
        foreach (var item in _parmaList)
        {
            if (item.gameObject.activeSelf)
            {
                item.ResetParma();

            }
        }
    }
}
