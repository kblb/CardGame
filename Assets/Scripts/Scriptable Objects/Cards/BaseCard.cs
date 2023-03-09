using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseCardScriptableObject : SerializedScriptableObject
{
    public string displayName;
    [TextArea] public string description;
    public Sprite icon;
    
    [FormerlySerializedAs("ethereal")] [InfoBox("Means that this card will go into used ethereal pile instead of discard pile.")]
    public bool isEthereal;
}