using System;
using Enemies;
using UnityEngine;

namespace Players
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField]
        private float health;
        public event Action<float> OnHealthChanged;
        
        public float Health {
            get => health;
            set {
                health = value;
                OnHealthChanged?.Invoke(value);
            }
        }

        public void AttackPlayer(Attack selectedAttack)
        {
            Health -= selectedAttack.Damage;
        }

        private void Start()
        {
            OnHealthChanged?.Invoke(Health);
        }
    }
}