using System;
using DG.Tweening;
using UnityEngine;

public class AnimationQueue : MonoBehaviour
{
    Sequence sequence;

    public void Init()
    {
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