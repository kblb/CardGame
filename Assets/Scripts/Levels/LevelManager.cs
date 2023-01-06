using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private Level currentLevel;
        [SerializeField, SceneObjectsOnly]
        private EnemyManager enemyManager;
    }
}