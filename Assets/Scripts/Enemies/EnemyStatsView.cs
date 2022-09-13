using UnityEngine;

namespace Enemies
{
    public class EnemyStatsView : MonoBehaviour
    {
        private EnemyModelInstance _enemy;
        
        public RectTransform healthBar;
        private float _defaultWidth;
        
        public void SetModel(EnemyModelInstance enemy)
        {
            _enemy = enemy;
            enemy.HealthChanged += OnHealthChanged;
            _defaultWidth = healthBar.sizeDelta.x;
        }

        public void OnHealthChanged(float maxHealth, float currentHealth, float shields)
        {
            var width = (currentHealth / maxHealth) * _defaultWidth;
            healthBar.sizeDelta = new Vector2(width, healthBar.sizeDelta.y);
        }
    }
}