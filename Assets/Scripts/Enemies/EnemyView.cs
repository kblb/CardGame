using Cards;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        private Enemy _enemy;

        public void Init(Enemy enemy)
        {
            _enemy = enemy;
            sprite.sprite = enemy.GetSprite;
        }
        
        // TODO: This shouldn't pass through View
        public bool Attack(Card card)
        {
            return _enemy.Damage(card);
        }
    }
}