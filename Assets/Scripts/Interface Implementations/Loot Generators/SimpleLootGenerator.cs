using UnityEngine;

public class SimpleLootGenerator : ILootGenerator
{
    public string name;
    public Sprite sprite;
    
    public AInventoryItemInstance Generate()
    {
        return new JewelInstance(name, sprite);
    }
}
