﻿using System.Collections.Generic;
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
    [SerializeField] private TMP_Text shieldsText;
    [SerializeField] public Transform buffBar;
    [SerializeField] public Transform intentBar;
    [SerializeField] [AssetsOnly] private Image buffBarImagePrefab;

    private Texture2D _healthBarImage;
    private float _oldHealth = float.NegativeInfinity;
    private float _oldShield = float.NegativeInfinity;

    public void Init(ActorInstance actor)
    {
        SetHealth(actor.scriptableObject.health, actor.currentHealth);
        SetShields(actor.currentShields);
        SetBuffs(actor.buffs);
        UpdateIntent(actor.inventory.deck.intent);
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

    public void UpdateIntent(IntentInstance intent)
    {
        foreach (Transform t in intentBar)
        {
            Destroy(t.gameObject);
        }

        if (intent != null)
        {
            Image intentImage = Instantiate(buffBarImagePrefab, intentBar);
            intentImage.sprite = intent.card.scriptableObject.intentIcon;
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

    public void SetShields(int shields)
    {
        shieldsText.text = "Shields\n" + shields;
        shieldsText.transform.parent.gameObject.SetActive(shields > 0);
    }

    public void SetHealth(float maxHealth, float currentHealth)
    {
        float shields = 0;
        if (Mathf.Abs(_oldHealth - currentHealth) > Mathf.Epsilon ||
            Mathf.Abs(_oldShield - shields) > Mathf.Epsilon)
        {
            _oldHealth = currentHealth;
            _oldShield = shields;

            healthText.text = "Health\n" + currentHealth.ToString(CultureInfo.InvariantCulture) +
                              (shields > 0 ? $"(+{shields})" : "") + "/" +
                              maxHealth.ToString(CultureInfo.InvariantCulture);

            float width = Mathf.Max(shields + currentHealth, maxHealth);
            int drawHp = Mathf.FloorToInt(currentHealth / width * ImageWidth);
            int drawShield = Mathf.FloorToInt(shields / width * ImageWidth);

            for (int i = 0; i < ImageWidth; i++)
            {
                if (i < drawHp)
                {
                    _healthBarImage.SetPixel(i, 0, Color.white);
                }
                else if (i < drawShield + drawHp)
                {
                    _healthBarImage.SetPixel(i, 0, Color.cyan);
                }
                else
                {
                    _healthBarImage.SetPixel(i, 0, Color.black);
                }
            }

            _healthBarImage.Apply();
        }
    }
}