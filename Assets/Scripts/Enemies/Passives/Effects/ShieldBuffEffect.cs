namespace Enemies
{
    public class ShieldBuffEffect : IEnemyPassiveEffect
    {
        private readonly int _amount;
        public ShieldBuffEffect(int amount)
        {

            _amount = amount;
        }

        public void ApplyEffect(EnemyController enemy)
        {
            enemy.Shield(_amount);
        }
    }
}