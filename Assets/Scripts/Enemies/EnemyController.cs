using System.Linq;
using Players;
using UnityEngine;

namespace Enemies
{
    // Class wrapping Enemy model and it's view
    public class EnemyController : MonoBehaviour
    {

        [SerializeField] private EnemyStatsView statsView;
        public EnemyModel RawEnemy { get; private set; }
        public EnemyModelInstance EnemyModelInstance { get; private set; }

        public Attack SelectedAttack => EnemyModelInstance.SelectedAttack;

        public void Init(EnemyModel enemy, EnemyModelInstance enemyModelInstance)
        {
            RawEnemy = enemy;
            EnemyModelInstance = enemyModelInstance;
            Instantiate(enemy.GetModel, this.transform);

            statsView.SetModel(EnemyModelInstance);
        }

        public void SelectNextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {
            EnemyModelInstance.SelectAttack(RawEnemy.GetNextAttack(playerModel, allEnemies, myEnemyIndex));
        }

        public void ApplyPassiveToQueue(PlayerModel playerModel, EnemyController[] queue, int myEnemyIndex)
        {
            RawEnemy.ApplyPassiveToQueue(playerModel, queue.Select(e => e.RawEnemy).ToArray(),
                queue.Select(e => e.EnemyModelInstance).ToArray(), myEnemyIndex);
        }

        public void Shield(float amount)
        {
            EnemyModelInstance.Shields += amount;
        }

        public void Heal(float flatHealAmount)
        {
            EnemyModelInstance.CurrentHealth = Mathf.Min(EnemyModelInstance.CurrentHealth + flatHealAmount,
                EnemyModelInstance.MaxHealth);
        }
    }
}