using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reAttacher : MonoBehaviour
{
    public Transform[] uiObjects;
    // Start is called before the first frame update
    void Start()
    {
        var vlg = gameObject.GetComponent<VerticalLayoutGroup>();
        vlg.enabled=false;
        foreach(Transform i in uiObjects)
        {
            i.parent = transform;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
