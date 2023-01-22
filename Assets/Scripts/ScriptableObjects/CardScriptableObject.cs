using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Card")]
public class CardScriptableObject : SerializedScriptableObject
{
    public string displayName;
    [TextArea] public string description;
    public Sprite icon;
    public Sprite intentIcon;
    public ICardAction cardAction;
}