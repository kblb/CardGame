using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/Actor")]
public class ActorScriptableObject : SerializedScriptableObject
{
    [SerializeField] public int health;
    [SerializeField] public GameObject prefab;
    [SerializeField] public List<CardScriptableObject> deck;
    [SerializeField] public ILootGenerator lootGenerator;
}