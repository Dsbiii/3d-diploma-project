using System;
using UnityEngine;

public class Panel : MonoBehaviour
{

    public event Action<Panel> OnOpen;
    public virtual void Open()
    {
        OnOpen?.Invoke(this);
    }
    public virtual void Close()
    {

    }

}
