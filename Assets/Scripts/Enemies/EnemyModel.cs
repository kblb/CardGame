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
            foreach (var (i, buff) in applied) passives[i].AddBuff(buff);
        }
    }

    public class EnemyModelInstance
    {
        private readonly List<IEnemyPassiveEffect> _buffs;
        private float _currentHealth;
        private float _maxHealth;
        private float _shields;

        public EnemyModelInstance(EnemyModel model)
        {
            MaxHealth = model.Health;
            CurrentHealth = model.Health;
            Shields = 0;
            _buffs = new List<IEnemyPassiveEffect>();
        }

        public float MaxHealth {
            get => _maxHealth;
            set {
                _maxHealth = value;
                OnHealthChanged();
            }
        }
        public float CurrentHealth {
            get => _currentHealth;
            set {
                _currentHealth = value;
                OnHealthChanged();
            }
        }
        public float Shields {
            get => _shields;
            set {
                _shields = value;
                OnHealthChanged();
            }
        }

        public IEnumerable<IEnemyPassiveEffect> Buffs => _buffs;
        public Attack SelectedAttack { get; private set; }
        public event Action<float, float, float> HealthChanged;
        public event Action<IEnumerable<IEnemyPassiveEffect>> BuffsChanged;
        public event Action<Attack> AttackChanged;

        public void AddBuff(IEnemyPassiveEffect passive)
        {
            _buffs.Add(passive);
            OnBuffsChanged();
        }

        public void ClearBuffs()
        {
            _buffs.Clear();
        }

        public void SelectAttack(Attack attack)
        {
            SelectedAttack = attack;
            OnAttackChanged();
        }
        private void OnAttackChanged()
        {
            AttackChanged?.Invoke(SelectedAttack);
        }

        private void OnHealthChanged()
        {
            HealthChanged?.Invoke(_maxHealth, _currentHealth, _shields);
        }

        private void OnBuffsChanged()
        {
            BuffsChanged?.Invoke(_buffs);
        }
    }

}