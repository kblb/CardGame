using System.Collections.Generic;
using System.Globalization;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActorStatsView : MonoBehaviour
{
    private const int ImageWidth = 240;

    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] public Transform buffBar;
    [SerializeField] public Transform intentBar;
    [SerializeField] [AssetsOnly] private Image buffBarImagePrefab;

    private Texture2D _healthBarImage;
    private float _oldHealth = float.NegativeInfinity;
    private float _oldShield = float.NegativeInfinity;

    public void Init(ActorInstance actor)
    {
        SetHealth(actor.scriptableObject.health, actor.currentHealth);
        SetBuffs(actor.buffs);
        SetIntent(actor.deck.intents);
    }

    private void Awake()
    {
        _healthBarImage = new Texture2D(ImageWidth, 1);
        for (int i = 0; i < ImageWidth; i++)
        {
            _healthBarImage.SetPixel(i, 0, Color.green);
        }

        _healthBarImage.Apply();
        healthBar.sprite = Sprite.Create(_healthBarImage, new Rect(0, 0, ImageWidth, 1), Vector2.zero);
    }

    public void SetIntent(List<CardInstance> intents)
    {
        foreach (Transform t in intentBar)
        {
            Destroy(t.gameObject);
        }

        foreach (CardInstance intent in intents)
        {
            Image intentImage = Instantiate(buffBarImagePrefab, intentBar);
            intentImage.sprite = intent.scriptableObject.intentIcon;
        }
    }

    private void SetBuffs(IEnumerable<BuffInstance> buffs)
    {
        foreach (Transform t in buffBar)
        {
            Destroy(t.gameObject);
        }

        foreach (BuffInstance buff in buffs)
        {
            Image buffImage = Instantiate(buffBarImagePrefab, buffBar);
            buffImage.sprite = buff.scriptableObject.icon;
        }
    }

    public void SetHealth(float maxHealth, float currentHealth)
    {
        float shields = 0;
        if (Mathf.Abs(_oldHealth - currentHealth) > Mathf.Epsilon ||
            Mathf.Abs(_oldShield - shields) > Mathf.Epsilon)
        {
            _oldHealth = currentHealth;
            _oldShield = shields;

            healthText.text = currentHealth.ToString(CultureInfo.InvariantCulture) +
                              (shields > 0 ? $"(+{shields})" : "") + "/" +
                              maxHealth.ToString(CultureInfo.InvariantCulture);

            float width = Mathf.Max(shields + currentHealth, maxHealth);
            int drawHp = Mathf.FloorToInt(currentHealth / width * ImageWidth);
            int drawShield = Mathf.FloorToInt(shields / width * ImageWidth);

            for (int i = 0; i < ImageWidth; i++)
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