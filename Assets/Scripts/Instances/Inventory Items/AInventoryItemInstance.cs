using UnityEngine;

public abstract class AInventoryItemInstance
{
    public readonly string name;
    public readonly Sprite sprite;

    public AInventoryItemInstance(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }
}
