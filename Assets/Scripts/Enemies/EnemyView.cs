using Cards;
using UnityEngine;

namespace Enemies
{
    public class EnemyView : MonoBehaviour
    {
        private Enemy _enemy;

        public void Init(Enemy enemy)
        {
            _enemy = enemy;
            Instantiate(enemy.GetModel, transform);
        }

        // TODO: This shouldn't pass through View
        public bool Attack(Card card)
        {
            return _enemy.Damage(card);
        }
    }
}