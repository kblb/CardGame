using System;
using UnityEngine;

public class BattlePhasePlayerAction : IBattlePhase
{
    public Action<bool> OnCommitReady;
    public Action OnFinish { get; set; }

    public void Start()
    {
        Debug.Log("--- Battle Phase: Player Action");

        //we're doing nothing here, waiting for the commit button
        OnCommitReady?.Invoke(true);
    }

    public void InvokeFinish()
    {
        OnCommitReady?.Invoke(false);
        OnFinish?.Invoke();
    }
}