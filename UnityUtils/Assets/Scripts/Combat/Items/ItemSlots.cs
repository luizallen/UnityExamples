[System.Serializable]
public class ItemSlots
{
    public ItemSlot Slot;
    public Item Item;
}

public enum ItemSlot
{
    None,
    Head,
    UpperBody,
    LowerBody,
    MainHand,
    OffHand,
    Bag1,
    Bag2
}
