using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class EnemyStatsView : MonoBehaviour
    {
        private const int ImageWidth = 240;

        public Image healthBar;
        private float _oldHealth = float.NegativeInfinity;
        private float _oldShield = float.NegativeInfinity;

        private Texture2D _healthBarImage;

        private void Awake()
        {
            _healthBarImage = new Texture2D(ImageWidth, 1);
            for (var i = 0; i < ImageWidth; i++)
            {
                _healthBarImage.SetPixel(i, 0, Color.green);
            }
            _healthBarImage.Apply();
            healthBar.sprite = Sprite.Create(_healthBarImage, new Rect(0, 0, ImageWidth, 1), Vector2.zero);
        }

        public void SetModel(EnemyModelInstance enemy)
        {
            enemy.HealthChanged += OnHealthChanged;
        }

        public void OnHealthChanged(float maxHealth, float currentHealth, float shields)
        {
            if (!(Mathf.Abs(_oldHealth - currentHealth) > Mathf.Epsilon) && !(Mathf.Abs(_oldShield - shields) > Mathf.Epsilon)) return;
            
            _oldHealth = currentHealth;
            _oldShield = shields;

            var width = Mathf.Max((shields + currentHealth), maxHealth);
            var drawHp = Mathf.FloorToInt((currentHealth / width) * ImageWidth);
            var drawShield = Mathf.FloorToInt((shields / width) * ImageWidth);
            
            for (var i = 0; i < ImageWidth; i++)
            {
                if (i < drawHp)
                {
                    _healthBarImage.SetPixel(i, 0, Color.green);
                }
                else if (i < drawShield + drawHp)
                {
                    _healthBarImage.SetPixel(i, 0, Color.blue);
                }
                else
                {
                    _healthBarImage.SetPixel(i, 0, Color.red);
                }
            }
            _healthBarImage.Apply();
        }
    }
}