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

    public CardInstance cardInstance;

    public DraggableImage draggableImage;

    private void Awake()
    {
        draggableImage = GetComponent<DraggableImage>();
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
        mask.SetActive(highlight);
    }
}