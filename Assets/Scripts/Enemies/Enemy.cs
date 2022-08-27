using Cards;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(menuName = "Create/Enemy")]
    public class Enemy : SerializedScriptableObject
    {
        [SerializeField] private float health;

        [OdinSerialize] private IEnemyAttack _attack;
        [OdinSerialize] private IEnemyPassive _passive;
        [OdinSerialize] private Sprite _sprite;
        public Sprite GetSprite => _sprite;
        
        public bool Damage(Card card)
        {
            Debug.Log($"Dealt {card.damage} damage to {name}");
            health -= card.damage;
            return health <= 0;
        }
    }

    public interface IEnemyAttack
    {
        Attack NextAttack();
    }

    public interface IEnemyPassive
    {
        Passive Passive();
    }
}