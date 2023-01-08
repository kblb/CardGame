using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class DamageIndicatorsView : MonoBehaviour
    {
        private static readonly Color[] playerColors =
        {
            Color.black, // 00
            Color.green, // 01
            Color.blue, // 10
            new(0.0f, 0.5f, 0.5f, 1.0f) // 11
        };
        private static readonly Color[] enemyColors =
        {
            Color.black, // 000
            Color.red, // 001
            Color.yellow, // 010
            new(1.0f, 0.5f, 0.0f, 1.0f), // 011
            Color.cyan, // 100
            new(0.5f, 0.0f, 1.0f, 1.0f), // 101
            new(0.5f, 0.0f, 1.0f, 1.0f), // 110
            new(0.5f, 0.0f, 1.0f, 1.0f), // 111
        };

        [SerializeField] [SceneObjectsOnly] private Image playerEffectIndicator;
        [SerializeField] [SceneObjectsOnly] private Image[] enemyEffectIndicators;

        public void SetIntents(Intent intent)
        {
            playerEffectIndicator.color = playerColors[(byte) intent.PlayerEffect];
            for (var i = 0; i < enemyEffectIndicators.Length; i++)
            {
                enemyEffectIndicators[i].color = enemyColors[(byte) intent.EnemyEffects[i]];
            }
        }

        public void SetIntents(Card card)
        {
            SetIntents(card.GetIntent());
        }
    }
}