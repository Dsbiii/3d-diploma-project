using Assets.Scripts.Instruments;
using Assets.Scripts.Stages.SecondStage.Dismantling;
using Assets.Scripts.Stages.SecondStage.Electric_Box;
using Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M;
using Assets.Scripts.Stages.SecondStage.Electric_Box.Parma;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBoxView : MonoBehaviour
{
    [SerializeField] private ElectricClampInstruments _electricClampInstruments;
    [SerializeField] private CE602MInstrument _cE602MInstrument;
    [SerializeField] private ParmaInstrument _parmaInstrument;
    [SerializeField] private ParmaComponent[] _parmaComponent;
    [SerializeField] private Door[] _doors;
    [SerializeField] private IKK _ikk;

    public void ResetParma()
    {
        foreach (var item in _parmaComponent)
        {
            item.ResetParma();
        }
    }

    public void PrepareBoxForSecondStage()
    {
        foreach(var item in _parmaComponent)
        {
            item.ResetParma();
        }
        //_ikk.Close();
        _electricClampInstruments.Close();
        _cE602MInstrument.Close();
        _parmaInstrument.Close();
    }
}
