using System.Collections.Generic;
using System.Globalization;
using Enemies.Passives.Effects;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class EnemyStatsView : MonoBehaviour
    {
        private const int ImageWidth = 240;

        [SerializeField] private Image healthBar;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private Transform buffBar;
        [SerializeField] private Transform intentBar;


        [SerializeField] [AssetsOnly] private Image buffBarImagePrefab;

        private Texture2D _healthBarImage;


        private float _oldHealth = float.NegativeInfinity;
        private float _oldShield = float.NegativeInfinity;

        private void Awake()
        {
            _healthBarImage = new Texture2D(ImageWidth, 1);
            for (var i = 0; i < ImageWidth; i++) _healthBarImage.SetPixel(i, 0, Color.green);
            _healthBarImage.Apply();
            healthBar.sprite = Sprite.Create(_healthBarImage, new Rect(0, 0, ImageWidth, 1), Vector2.zero);
        }

        public void SetModel(EnemyModelInstance enemy)
        {
            enemy.HealthChanged += OnHealthChanged;
            enemy.BuffsChanged += OnBuffsChanged;
            enemy.AttackChanged += OnAttackChanged;
            OnHealthChanged(enemy.MaxHealth, enemy.CurrentHealth, enemy.Shields);
            OnBuffsChanged(null);
            OnAttackChanged(null);
        }

        private void OnAttackChanged(Attack attack)
        {
            foreach (Transform t in intentBar) Destroy(t.gameObject);

            if (attack == null) return;

            var intent = Instantiate(buffBarImagePrefab, intentBar);
            intent.sprite = attack.Icon;
        }

        private void OnBuffsChanged(IEnumerable<IEnemyPassiveEffect> buffs)
        {
            foreach (Transform t in buffBar) Destroy(t.gameObject);

            if (buffs == null) return;

            foreach (var buff in buffs)
            {
                var buffImage = Instantiate(buffBarImagePrefab, buffBar);
                buffImage.sprite = buff.Icon;
            }
        }

        public void OnHealthChanged(float maxHealth, float currentHealth, float shields)
        {
            if (Mathf.Abs(_oldHealth - currentHealth) <= Mathf.Epsilon && Mathf.Abs(_oldShield - shields) <= Mathf.Epsilon) return;

            _oldHealth = currentHealth;
            _oldShield = shields;

            healthText.text = currentHealth.ToString(CultureInfo.InvariantCulture) + (shields > 0 ? $"(+{shields})" : "") + "/" + maxHealth.ToString(CultureInfo.InvariantCulture);

            var width = Mathf.Max(shields + currentHealth, maxHealth);
            var drawHp = Mathf.FloorToInt(currentHealth / width * ImageWidth);
            var drawShield = Mathf.FloorToInt(shields / width * ImageWidth);

            for (var i = 0; i < ImageWidth; i++)
                if (i < drawHp)
                    _healthBarImage.SetPixel(i, 0, Color.green);
                else if (i < drawShield + drawHp)
                    _healthBarImage.SetPixel(i, 0, Color.blue);
                else
                    _healthBarImage.SetPixel(i, 0, Color.red);
            _healthBarImage.Apply();
        }
    }
}