using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Players
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private PlayerView view;

        [SerializeField] [SceneObjectsOnly] private PlayerModel model;

        public PlayerModel Model => model;

        public void Init()
        {
            view.Init(model);
        }

        public void AttackPlayer(Attack selectedAttack)
        {
            model.AttackPlayer(selectedAttack);
        }
    }
}