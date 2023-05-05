using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Jewel")]
public class JewelScriptableObject : SerializedScriptableObject
{
    public readonly string name;
    public readonly Sprite sprite;
    public IModify modifier;
}