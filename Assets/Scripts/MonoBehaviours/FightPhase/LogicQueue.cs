using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LogicQueue
{
    private Sequence sequence;
    private readonly List<Action> actionsInQueue = new();

    public void AddElement(Action action)
    {
        actionsInQueue.Add(action);
        if (sequence == null || sequence.IsPlaying() == false)
        {
            CreateNewSequence();
        }
    }

    private void CreateNewSequence()
    {
        sequence = DOTween.Sequence();
        sequence.OnComplete(CreateNewSequence);

        foreach (Action action in actionsInQueue)
        {
            sequence.AppendCallback(() =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    throw e;
                }
            });
            sequence.AppendInterval(0.5f);
        }

        actionsInQueue.Clear();
    }
}