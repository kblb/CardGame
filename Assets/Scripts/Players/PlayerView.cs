using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private EnemyStatsView statsView;
        [SerializeField] [SceneObjectsOnly] private GameObject modelPrefab;
        private float originalScale;
        private const float AttackScaleFactor = 1.2f;
        private PlayerModel model;

        public void Init(PlayerModel model)
        {
            originalScale = modelPrefab.transform.localScale.x;
            this.model = model;
            model.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(float newValue)
        {
            statsView.OnHealthChanged(model.maxHealth, newValue, 0);
        }

        public void ShowAttackAnimation()
        {
            modelPrefab.transform.localScale = Vector3.one * (originalScale * AttackScaleFactor);
        }

        public void HideAttackAnimation()
        {
            modelPrefab.transform.localScale = Vector3.one * originalScale;
        }
    }
}