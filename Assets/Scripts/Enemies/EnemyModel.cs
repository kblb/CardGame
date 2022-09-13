using System;
using System.Collections.Generic;
using Enemies.Attacks;
using Enemies.Passives;
using Enemies.Passives.Effects;
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

        public void ApplyPassiveToQueue(PlayerModel playerModel, EnemyModel[] enemies, EnemyModelInstance[] passives, int myEnemyIndex)
        {
            var applied = _passive.Passive(playerModel, enemies, myEnemyIndex);
            for (var i = 0; i < applied.Count; i++)
            {
                passives[i].AddBuff(applied[i]);
            }
        }
    }

    public class EnemyModelInstance
    {
        private readonly List<IEnemyPassiveEffect> buffs;
        private float maxHealth;
        private float currentHealth;
        private float shields;
        public event Action<float, float, float> HealthChanged;

        public float MaxHealth {
            get => maxHealth;
            set {
                maxHealth = value;
                OnHealthChanged();
            }
        }
        public float CurrentHealth {
            get => currentHealth;
            set {
                currentHealth = value;
                OnHealthChanged();
            }
        }
        public float Shields {
            get => shields;
            set {
                shields = value;
                OnHealthChanged();
            }
        }

        public IEnumerable<IEnemyPassiveEffect> Buffs => buffs;

        public EnemyModelInstance(EnemyModel model)
        {
            MaxHealth = model.Health;
            CurrentHealth = model.Health;
            Shields = 0;
            buffs = new List<IEnemyPassiveEffect>();
        }
        
        public void AddBuff(IEnemyPassiveEffect passive)
        {
            buffs.Add(passive);
        }

        public void ClearBuffs()
        {
            buffs.Clear();
        }

        private void OnHealthChanged()
        {
            HealthChanged?.Invoke(maxHealth, currentHealth, shields);
        }
    }

}