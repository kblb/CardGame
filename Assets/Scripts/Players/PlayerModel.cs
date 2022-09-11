using System;
using Enemies;
using UnityEngine;

namespace Players
{
    public class PlayerModel : MonoBehaviour
    {
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
            OnHealthChanged?.Invoke(Health);
        }
        public event Action<float> OnHealthChanged;

        public void AttackPlayer(Attack selectedAttack)
        {
            Health -= selectedAttack.Damage;
        }
    }
}