using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlePhaseEnemiesMoveForward : IBattlePhase
{
    private readonly LogicQueue logicQueue;
    private readonly List<SlotInstance> slots;

    public BattlePhaseEnemiesMoveForward(List<SlotInstance> slots, LogicQueue logicQueue)
    {
        this.slots = slots;
        this.logicQueue = logicQueue;
    }

    public Action OnFinish { get; set; }

    public void Start()
    {
        Debug.Log("--- Battle Phase: Enemies Move Forward");
        logicQueue.AddElement(0.5f, () =>
        {
            for (int i = 0; i < slots.Count - 1; i++)
            {
                SlotInstance slot = slots[i];
                SlotInstance nextSlot = slots[i + 1];
                if (slot.IsFree())
                {
                    slot.PlaceActorHere(nextSlot.actor);
                    nextSlot.PlaceActorHere(null);
                }
            }

            OnFinish?.Invoke();
        });
    }
}