using UnityEngine;

public class SimpleLootGenerator : ILootGenerator
{
    public JewelScriptableObject jewel;
    
    public JewelInstance Generate()
    {
        return new JewelInstance(jewel);
    }
}
