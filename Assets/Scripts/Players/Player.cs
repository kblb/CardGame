using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour
    {
        private float _health;
        public void DealDamage(float damage)
        {
            _health -= damage;
        }
    }
}