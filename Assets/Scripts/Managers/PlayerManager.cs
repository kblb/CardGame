using Players;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private Player _player;

        public void DealDamage(float damage)
        {
            if (_player == null)
            {
                _player.DealDamage(damage);
            }
        }
    }
}