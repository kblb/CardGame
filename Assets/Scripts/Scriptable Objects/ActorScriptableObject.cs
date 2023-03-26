using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Fight Phase/Actor")]
public class ActorScriptableObject : SerializedScriptableObject
{
    [SerializeField] public int health;
    [SerializeField] public GameObject prefab;
    [SerializeField] public List<BaseCardScriptableObject> deck;

    [SerializeField] public ActorScriptableObject spawnAfterDeath;
}