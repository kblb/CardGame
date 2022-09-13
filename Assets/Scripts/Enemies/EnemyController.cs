using System.Collections.Generic;
using System.Linq;
using Cards;
using Enemies.Passives.Effects;
using Players;
using UnityEngine;

namespace Enemies
{
    // Class wrapping Enemy model and it's view
    public class EnemyController : MonoBehaviour
    {
        public EnemyModel RawEnemy { get; private set; }
        public EnemyModelInstance EnemyModelInstance { get; private set; }

        public Attack SelectedAttack { get; private set; }
        public float Shields => EnemyModelInstance.Shields;
        public float Health => EnemyModelInstance.CurrentHealth;
        public float MaxHealth => EnemyModelInstance.MaxHealth;

        // Extract this to EnemyView
        public void Init(EnemyModel enemy, EnemyModelInstance enemyModelInstance)
        {
            RawEnemy = enemy;
            EnemyModelInstance = enemyModelInstance;
            Instantiate(enemy.GetModel, transform);
        }

        public void SelectNextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {
            SelectedAttack = RawEnemy.GetNextAttack(playerModel, allEnemies, myEnemyIndex);
        }

        public void ApplyPassiveToQueue(PlayerModel playerModel, EnemyController[] queue, int i)
        {
            RawEnemy.ApplyPassiveToQueue(playerModel, queue.Select(e => e.RawEnemy).ToArray(), queue.Select(e => e.EnemyModelInstance).ToArray(), i);
        }

        public void Shield(float amount)
        {
            EnemyModelInstance.Shields += amount;
        }

        public void Heal(float flatHealAmount)
        {
            EnemyModelInstance.CurrentHealth = Mathf.Min(Health + flatHealAmount, MaxHealth);
        }

        public bool AttackEnemy(Card card)
        {
            EnemyModelInstance.CurrentHealth -= card.damage;
            return Health > 0;
        }
    }
}