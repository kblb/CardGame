using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JewelView : MonoBehaviour, ITooltipable
{
    public JewelInstance instance;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Image image;

    public void Initialize(JewelInstance item)
    {
        instance = item;
        titleText.text = item.name;
        image.sprite = item.sprite;
    }

    public string GetTooltipText()
    {
        return "This is an item instance " + this.GetType().ToString() + "with name " + instance.name;
    }
}