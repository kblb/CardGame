using Enemies;
using Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] public PlayerController playerController;
        public PlayerModel PlayerModel => playerController.Model;

        private void Awake()
        {
            playerController.Init();
        }

        public void AttackPlayer(Attack selectedAttack)
        {
            playerController.AttackPlayer(selectedAttack);
        }
    }
}