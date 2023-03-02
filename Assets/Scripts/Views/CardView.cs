using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DraggableImage))]
public class CardView : MonoBehaviour
{
    [SerializeField] private Image cardIcon;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private GameObject mask;
    [SerializeField] private Image highlightImage;

    public CardInstance cardInstance;

    public DraggableImage draggableImage;

    private TweenerCore<float, float, FloatOptions> currentTween;

    private void Awake()
    {
        draggableImage = GetComponent<DraggableImage>();
        Highlight(false);
    }

    public void Init(CardInstance card)
    {
        this.cardInstance = card;
        cardIcon.sprite = card.scriptableObject.icon;
        titleText.text = card.scriptableObject.displayName;
        descriptionText.text = card.scriptableObject.description;
        
    }

    public void Highlight(bool highlight)
    {
        if (currentTween != null)
        {
            currentTween.Kill();
        }
        
        if (highlight)
        {
            Color color = highlightImage.color;
            color.a = 0;
            highlightImage.color = color;
            currentTween = DOTween.To(() => highlightImage.color.a, x => highlightImage.color = new Color(color.r, color.g, color.b, x), 1, 1);
        }

        mask.SetActive(highlight);
    }
}