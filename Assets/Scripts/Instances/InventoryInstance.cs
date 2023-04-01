using System.Collections.Generic;

public class InventoryInstance
{
    public DeckInstance deck;
    public List<JewelInstance> jewels = new List<JewelInstance>();

    public void AddJewel(JewelInstance jewel)
    {
        jewels.Add(jewel);
    }
}
