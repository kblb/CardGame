using System;
using System.Collections.Generic;

public class InventoryInstance
{
    public DeckInstance deck;
    public List<JewelInstance> jewels = new List<JewelInstance>();

    public event Action<JewelInstance> OnJewelRemoved;
    
    public void AddJewel(JewelInstance jewel)
    {
        jewels.Add(jewel);
    }

    public void RemoveJewel(JewelInstance jewel)
    {
        jewels.Remove(jewel);
        OnJewelRemoved?.Invoke(jewel);
    }
}
