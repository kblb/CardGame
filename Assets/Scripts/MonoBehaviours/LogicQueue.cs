using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LogicQueue
{
    private Sequence sequence;
    private readonly List<KeyValuePair<Action, float>> actionsInQueue = new();

    public void AddElement(float delay, Action action)
    {
        actionsInQueue.Add(new KeyValuePair<Action, float>(action, delay));
        if (sequence == null || sequence.IsPlaying() == false)
        {
            CreateNewSequence();
        }
    }

    private void CreateNewSequence()
    {
        sequence = DOTween.Sequence();
        sequence.OnComplete(CreateNewSequence);

        foreach (KeyValuePair<Action, float> action in actionsInQueue)
        {
            sequence.AppendCallback(() =>
            {
                try
                {
                    action.Key();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            });
            sequence.AppendInterval(action.Value);
        }

        actionsInQueue.Clear();
    }
}