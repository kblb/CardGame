using System.Globalization;
using Enemies;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private EnemyStatsView statsView;
        private PlayerModel model;

        public void Init(PlayerModel model)
        {
            this.model = model;
            model.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(float newValue)
        {
            statsView.OnHealthChanged(model.maxHealth, newValue, 0);
        }
    }
}