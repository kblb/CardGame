using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : InteractableMonoBehaviour<CardView>
{
    [SerializeField] private Image cardIcon;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private GameObject mask;
    [SerializeField] private Image highlightImage;

    public CardInstance cardInstance;

    private TweenerCore<float, float, FloatOptions> currentTween;
    private Vector3 originalPosition;
    private Vector3 originalAngle;
    private int originalSiblingIndex;

    private new void Awake()
    {
        base.Awake();
        Highlight(false);
    }

    public void Init(CardInstance attackCard)
    {
        this.cardInstance = attackCard;
        cardIcon.sprite = attackCard.scriptableObject.icon;
        titleText.text = attackCard.scriptableObject.displayName;
        descriptionText.text = attackCard.scriptableObject.description;
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

    public void MoveUp()
    {
        Vector3 position = originalPosition;
        position.y += 50;

        transform.DOKill();
        transform.DOMove(position, .2f).SetEase(Ease.OutBack);
        transform.DORotate(new Vector3(0, 0, 0), .5f).SetEase(Ease.OutBack);
        transform.DOScale(1.2f, .5f).SetEase(Ease.OutBack);
        transform.SetAsLastSibling();
    }

    public void NewPosition(Vector3 position, Vector3 angle, int siblingIndex, float duration)
    {
        transform.DOKill();
        this.originalPosition = position;
        this.originalAngle = angle;
        this.originalSiblingIndex = siblingIndex;
        transform.DOMove(position, duration);
        transform.DORotate(angle, duration);
        transform.SetSiblingIndex(siblingIndex);
    }

    public void RestoreToOriginalPosition()
    {
        transform.DOKill();
        transform.DOMove(originalPosition, .5f);
        transform.DORotate(originalAngle, .5f);
        transform.DOScale(1, .5f);
        transform.SetSiblingIndex(this.originalSiblingIndex);
    }
}