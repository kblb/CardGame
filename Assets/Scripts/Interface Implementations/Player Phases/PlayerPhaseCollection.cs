using System;
using System.Linq;

public class PlayerPhaseCollection : IPlayerPhase
{
    private readonly IPlayerPhase[] collection;

    public event Action OnCancel;
    public event Action OnCompleted;

    public PlayerPhaseCollection(IPlayerPhase[] collection)
    {
        this.collection = collection;
        for (int i = 0; i < collection.Length; i++)
        {
            collection[i].OnCancel += Start;
            
            if (i < collection.Length - 1)
            {
                collection[i].OnCompleted += collection[i + 1].Start;
            }

            if (i == collection.Length - 1)
            {
                collection[i].OnCompleted += InvokeOnCompleted;
            }
        }
    }

    private void InvokeOnCompleted()
    {
        OnCompleted?.Invoke();
    }

    public void Start()
    {
        collection.First().Start();
    }
}
