using Cards;
using Players;
using UnityEngine;

namespace Enemies
{
    // Class wrapping Enemy model and it's view
    public class EnemyController : MonoBehaviour
    {
        public Enemy RawEnemy { get; private set; }
        
        public Attack SelectedAttack { get; private set; }

        // Extract this to EnemyView
        public void Init(Enemy enemy)
        {
            RawEnemy = enemy;
            Instantiate(enemy.GetModel, transform);
        }
        
        public void SelectNextAttack(PlayerModel playerModel, Enemy[] allEnemies, int myEnemyIndex)
        {
            SelectedAttack = RawEnemy.GetNextAttack(playerModel, allEnemies, myEnemyIndex);
        }

        public bool AttackEnemy(Card card)
        {
            return RawEnemy.Damage(card);
        }
    }
}