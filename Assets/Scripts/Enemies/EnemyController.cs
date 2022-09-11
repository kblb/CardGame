using System.Collections.Generic;
using System.Linq;
using Cards;
using Players;
using UnityEngine;

namespace Enemies
{
    // Class wrapping Enemy model and it's view
    public class EnemyController : MonoBehaviour
    {
        public EnemyModel RawEnemy { get; private set; }

        public Attack SelectedAttack { get; private set; }
        public List<IEnemyPassiveEffect> Buffs { get; private set; }

        public float Shields { get; private set; }
        public float Health { get; private set; }
        public float MaxHealth { get; private set; }

        // Extract this to EnemyView
        public void Init(EnemyModel enemy)
        {
            RawEnemy = enemy;
            Instantiate(enemy.GetModel, transform);
            Health = enemy.Health;
            MaxHealth = enemy.Health;
        }

        public void SelectNextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {
            SelectedAttack = RawEnemy.GetNextAttack(playerModel, allEnemies, myEnemyIndex);
        }

        public void ApplyPassiveToQueue(PlayerModel playerModel, EnemyController[] queue, int i)
        {
            RawEnemy.ApplyPassiveToQueue(playerModel, queue.Select(e => e.RawEnemy).ToArray(), queue.Select(e => e.Buffs).ToArray(), i);
        }

        public void Shield(float amount)
        {
            Shields += amount;
        }

        public void Heal(float flatHealAmount)
        {
            Health = Mathf.Min(Health + flatHealAmount, MaxHealth);
        }

        public bool AttackEnemy(Card card)
        {
            Health -= card.damage;
            return Health > 0;
        }
    }
}