using UnityEngine;

public class JewelInstance
{
    public readonly string name;
    public readonly Sprite sprite;

    public JewelInstance(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }
}