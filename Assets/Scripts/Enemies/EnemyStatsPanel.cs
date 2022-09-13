using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public class EnemyStatsPanel : MonoBehaviour
    {
        [SerializeField] [AssetsOnly] EnemyStatsView enemyStatsViewPrefab;
        
        public EnemyStatsView AddPanel(EnemyModelInstance enemy)
        {
            var enemyStatsView = Instantiate(enemyStatsViewPrefab, transform);
            enemyStatsView.SetModel(enemy);
            return enemyStatsView;
        }
    }
}