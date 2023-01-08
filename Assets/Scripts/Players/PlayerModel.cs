using System;
using Enemies;
using UnityEngine;

namespace Players
{
    public class PlayerModel : MonoBehaviour
    {
        public float maxHealth { get; private set; }
        [SerializeField] private float health;

        public float Health {
            get => health;
            set {
                health = value;
                OnHealthChanged?.Invoke(value);
            }
        }

        private void Start()
        {
            maxHealth = health;
            OnHealthChanged?.Invoke(Health);
        }
        public event Action<float> OnHealthChanged;

        public void AttackPlayer(Attack selectedAttack)
        {
            Debug.Log($"Player receiving {selectedAttack.Damage} damage");
            Health -= selectedAttack.Damage;
        }
    }
}