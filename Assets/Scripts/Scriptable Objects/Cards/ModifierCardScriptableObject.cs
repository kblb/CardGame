using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Modifier Card")]
public class ModifierCardScriptableObject : BaseCardScriptableObject
{
    public IModify modify;
}