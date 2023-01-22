using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Fight Phase/Actor")]
public class ActorScriptableObject : SerializedScriptableObject
{
    [SerializeField] public int health;
    [SerializeField] public ActorView prefab;
    [SerializeField] public List<CardScriptableObject> deck;
}