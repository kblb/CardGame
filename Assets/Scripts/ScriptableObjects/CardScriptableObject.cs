using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Create/Card")]
public class CardScriptableObject : SerializedScriptableObject
{
    public string displayName;
    [TextArea] public string description;
    public Sprite icon;
    public Sprite intentIcon;
    public ICardAction cardAction;

    [InfoBox("Means that this card will go into used ethereal pile instead of discard pile.")]
    public bool ethereal;
}