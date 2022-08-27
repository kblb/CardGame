namespace Enemies.Attacks
{
    public class TestAttack : IEnemyAttack
    {
        public Attack NextAttack()
        {
            return new Attack
            {
                Damage = 10,
                Effect = false
            };
        }
    }
}