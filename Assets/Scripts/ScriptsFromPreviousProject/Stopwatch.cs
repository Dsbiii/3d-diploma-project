using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    public Act _Act;
    public Stage _StageStopwatch;
    public GameObject Arrow;
    public bool runTime;
    public float _Secund;
    public bool IsUssedWatch;
    public int _Cout_Inp;

    public enum Stage
    {
        StartWatch,
        Playwatch,
        PauseWatch,
        ResetWatch
    }


    private void Update()
    {
        if (_StageStopwatch == Stage.Playwatch)
        {
            Arrow.transform.Rotate(0, 0, 6 * Time.deltaTime);
            _Secund = _Secund += 1 * Time.deltaTime;
            runTime = true;
            IsUssedWatch = true;


        }
        else if(_StageStopwatch == Stage.ResetWatch)
        {
            Arrow.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _Secund = 0;
            _Cout_Inp = 0;
            _Act.Instrumental._Sec_Time = "";
            _Act.Instrumental._Cout_Inp = "";
            _StageStopwatch = Stage.StartWatch;
            IsUssedWatch = true;
        }
    }

    public void OnPointerDown()
    {
        runTime = false;
        IsUssedWatch = false;
        _StageStopwatch += 1;
        _Secund = Mathf.Round(_Secund);
        _Act.Instrumental._Sec_Time = _Secund.ToString();
        _Act.Instrumental._Cout_Inp = _Cout_Inp.ToString();
    }

}
