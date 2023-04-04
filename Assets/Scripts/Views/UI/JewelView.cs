using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JewelView : InteractableMonoBehaviour<JewelView>
{
    public JewelInstance instance;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Image image;

    public void Initialize(JewelInstance item)
    {
        instance = item;
        titleText.text = item.scriptableObject.name;
        image.sprite = item.scriptableObject.sprite;
    }
}