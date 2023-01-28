using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Battle")]
public class BattleScriptableObject : SerializedScriptableObject
{
    public List<ActorScriptableObject> enemies;
    public int enemySlots;
}