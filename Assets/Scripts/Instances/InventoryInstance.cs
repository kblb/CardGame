using System;
using System.Collections.Generic;

public class InventoryInstance
{
    public DeckInstance deck;
    public List<AInventoryItemInstance> items = new List<AInventoryItemInstance>();

    public void AddItem(AInventoryItemInstance item)
    {
        items.Add(item);
    }
}
