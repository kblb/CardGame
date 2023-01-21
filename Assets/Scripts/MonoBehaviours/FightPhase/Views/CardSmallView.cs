using UnityEngine;
using UnityEngine.UI;

public class CardSmallView : MonoBehaviour
{
    [SerializeField] private Image cardIcon;

    public void SetIcon(Sprite icon)
    {
        cardIcon.sprite = icon;
    }
}