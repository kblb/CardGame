using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;

        public void Init(Enemy enemy)
        {
            sprite.sprite = enemy.GetSprite;
        }
    }
}