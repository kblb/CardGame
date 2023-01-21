using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Fight Phase/Fight Phase")]
public class FightPhaseScriptableObject : SerializedScriptableObject
{
    public List<ActorScriptableObject> enemies;
}