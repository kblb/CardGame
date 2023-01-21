using System;
using DG.Tweening;
using UnityEngine;

public class AnimationQueue 
{
    private Sequence sequence;
    private Transform transform;

    public AnimationQueue(Transform transform)
    {
        this.transform = transform;
        sequence = DOTween.Sequence();
    }

    public void AddElement(Action action)
    {
        if (sequence.IsPlaying() == false)
        {
            sequence = DOTween.Sequence();
        }
        sequence.AppendCallback(() =>
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        });
        sequence.AppendInterval(0.5f);
    }

}