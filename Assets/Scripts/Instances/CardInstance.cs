using System;
using System.Collections.Generic;

public class CardInstance
{
    public readonly CardScriptableObject scriptableObject;
    public List<JewelInstance> jewels = new List<JewelInstance>();

    public event Action<JewelInstance> OnJewelAdded;
    
    public CardInstance(CardScriptableObject scriptableObject)
    {
        if (scriptableObject == null)
        {
            throw new Exception("Scriptable object can't be null.");
        }
        this.scriptableObject = scriptableObject;
    }

    public void InsertJewel(JewelInstance jewel)
    {
        jewels.Add(jewel);
        OnJewelAdded?.Invoke(jewel);
    }
}
