using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Fight Phase/Buff")]
public class BuffScriptableObject : SerializedScriptableObject
{
    public Sprite icon;
    public IBuff buff;
}