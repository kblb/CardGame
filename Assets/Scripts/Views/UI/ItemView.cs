using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public AInventoryItemInstance instance;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Image image;

    public void Initialize(AInventoryItemInstance item)
    {
        instance = item;
        titleText.text = item.name;
        image.sprite = item.sprite;
    }
}