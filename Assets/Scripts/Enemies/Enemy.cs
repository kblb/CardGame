using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(menuName = "Create/Enemy")]
    public class Enemy : SerializedScriptableObject
    {
        [SerializeField] private int health;

        [OdinSerialize] private IEnemyAttack _attack;
        [OdinSerialize] private IEnemyPassive _passive;
        [OdinSerialize] private Sprite _sprite;
        public Sprite GetSprite => _sprite;
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