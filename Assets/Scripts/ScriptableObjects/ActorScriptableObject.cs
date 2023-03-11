using System.Collections.Generic;
using Builders;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Fight Phase/Actor")]
public class ActorScriptableObject : SerializedScriptableObject
{
    [SerializeField] public int health;
    /// TODO: Change this to `resistances` and probably buffs
    [SerializeField] public Affinity affinity;
    [SerializeField] public GameObject prefab;
    [SerializeField] public List<CardScriptableObject> deck;
}