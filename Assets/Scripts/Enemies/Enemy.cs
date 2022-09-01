using Cards;
using Players;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(menuName = "Create/Enemy")]
    public class Enemy : SerializedScriptableObject
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;

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

        public void Heal(float flatHealAmount)
        {
            health = Mathf.Min(health + flatHealAmount, maxHealth);
        }
    }

    public interface IEnemyAttack
    {
        Attack NextAttack(Player player, Enemy[] allEnemies, int myEnemyIndex);
    }

    public interface IEnemyPassive
    {
        Passive Passive();
    }
}