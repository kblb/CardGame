using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Create/Card")]
public class CardScriptableObject : SerializedScriptableObject
{
    public Sprite intentIcon;
    public ICast cast;
    
    public string displayName;
    [TextArea] public string description;
    public Sprite icon;
    
    [FormerlySerializedAs("ethereal")] [InfoBox("Means that this card will go into used ethereal pile instead of discard pile.")]
    public bool isEthereal;
}