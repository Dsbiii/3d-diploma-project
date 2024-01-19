using System.Collections;
using UnityEngine;

public enum StageState
{
    Dismantling,
    Inspection
}

public class SecondStageState : MonoBehaviour
{
    private StageState _stageState = StageState.Inspection;

    public StageState StageState => _stageState;

    public void EnterInInspection()
    {
        _stageState = StageState.Inspection;
    }

    public void EnterInDismantlingState()
    {
        _stageState = StageState.Dismantling;
    }
}