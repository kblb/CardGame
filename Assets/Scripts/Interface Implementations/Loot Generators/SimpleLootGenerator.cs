using UnityEngine;

public class SimpleLootGenerator : ILootGenerator
{
    public string name;
    public Sprite sprite;
    
    public JewelInstance Generate()
    {
        return new JewelInstance(name, sprite);
    }
}
