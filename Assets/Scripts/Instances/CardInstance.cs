using System;
using System.Collections.Generic;

public class CardInstance
{
    public readonly CardScriptableObject scriptableObject;

    public CardInstance(CardScriptableObject scriptableObject)
    {
        if (scriptableObject == null)
        {
            throw new Exception("Scriptable object can't be null.");
        }
        this.scriptableObject = scriptableObject;
    }
}
