using System.Collections.Generic;
using Players;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(menuName = "Create/Enemy")]
    public class EnemyModel : SerializedScriptableObject
    {
        [SerializeField] private float health;

        [OdinSerialize] private IEnemyAttack _attack;
        [OdinSerialize] private GameObject _enemyPrefab;
        [OdinSerialize] private IEnemyPassive _passive;

        public GameObject GetModel => _enemyPrefab;
        public float Health => health;

        public Attack GetNextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {
            return _attack.NextAttack(playerModel, allEnemies, myEnemyIndex);
        }

        public void ApplyPassiveToQueue(PlayerModel playerModel, EnemyModel[] enemies, List<IEnemyPassiveEffect>[] passives, int myEnemyIndex)
        {
            var applied = _passive.Passive(playerModel, enemies, myEnemyIndex);
            foreach (var (enemyIndex, passive) in applied) passives[enemyIndex].Add(passive);
        }
    }

}