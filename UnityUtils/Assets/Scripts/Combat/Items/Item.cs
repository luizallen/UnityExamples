using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemSlot PrimarySlot;
    public ItemSlot SecondarySlot;
    public bool UseBoth;
    public Sprite Icon;

    public abstract void Use(Unit unit);
}
