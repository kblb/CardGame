using Players;
using UnityEngine;

namespace Enemies.Attacks
{
    public class BasicHit : IEnemyAttack
    {
        public float Damage;
        public Sprite Icon;

        public Attack NextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {

            return myEnemyIndex == 0 ? new Attack(Damage, null, Icon) : null;
        }
    }
}