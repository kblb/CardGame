using System;
using UnityEngine;

public class BattlePhaseEnemiesShowIntent : IBattlePhase
{
    public Action OnFinish { get; set; }
    public void Start()
    {
        Debug.Log("--- Battle Phase: Enemies Show Intent");
        throw new NotImplementedException();
    }
}