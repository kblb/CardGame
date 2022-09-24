using Players;
using Registries;

namespace Enemies.Attacks
{
    public class BasicHit : IEnemyAttack
    {
        public float Damage;

        public Attack NextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {

            return myEnemyIndex == 0 ? new Attack(Damage, null, BattleIconRegistry.SwordIcon) : null;
        }
    }
}