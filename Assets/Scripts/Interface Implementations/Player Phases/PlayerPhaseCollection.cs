using System;
using System.Linq;
using UnityEngine;

public class PlayerPhaseCollection : IPlayerPhase
{
    private readonly IPlayerPhase[] collection;

    private int currentIndex;

    public event Action OnCancel;
    public event Action OnCompleted;

    public PlayerPhaseCollection(IPlayerPhase[] collection)
    {
        this.collection = collection;
        for (int i = 0; i < collection.Length; i++)
        {
            collection[i].OnCancel += () =>
            {
                currentIndex = 0;
                Start();
            };
            
            if (i < collection.Length - 1)
            {
                int localVariable = i;
                collection[i].OnCompleted += () =>
                {
                    currentIndex = localVariable + 1;
                    collection[localVariable + 1].Start();
                };
            }

            if (i == collection.Length - 1)
            {
                collection[i].OnCompleted += () =>
                {
                    currentIndex = -1;
                    InvokeOnCompleted();
                };
            }
        }
    }

    private void InvokeOnCompleted()
    {
        Debug.Log("Invoking on complete player phase action.");
        OnCompleted?.Invoke();
    }

    public void Start()
    {
        collection.First().Start();
    }

    public void Terminate()
    {
        collection[currentIndex].Terminate();
    }
}
