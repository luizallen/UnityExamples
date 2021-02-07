using System;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public List<ItemSlots> ItemSlots;

    void Awake()
        => SetItemSlots();

    public void Equip(Item item)
    {
        var primary = ItemSlots[(int)item.PrimarySlot];
        ItemSlots secondary = null;

        if (item.SecondarySlot != ItemSlot.None)
            secondary = ItemSlots[(int)item.SecondarySlot];

        if (item.UseBoth)
        {
            if (primary.Item == null && secondary.Item == null)
            {
                primary.Item = secondary.Item = item;
                ActivateEquippable(item);
            }
        }
        else
        {
            if (primary.Item == null)
            {
                primary.Item = item;
                ActivateEquippable(item);
            }
            else if (item.SecondarySlot != ItemSlot.None && secondary.Item == null)
            {
                secondary.Item = item;
                ActivateEquippable(item);
            }
        }
    }

    public void UnEquip(Item item)
    {
        foreach (var slot in ItemSlots)
        {
            if(slot.Item == item)
            {
                slot.Item = null;
                Destroy(item.gameObject);
            }
        }
    }

    public Item GetItem(ItemSlot slot)
        => ItemSlots[(int)slot].Item;

    void SetItemSlots()
    {
        ItemSlots = new List<ItemSlots>();

        foreach (ItemSlot item in Enum.GetValues(typeof(ItemSlot)))
        {
            var slot = new ItemSlots();
            slot.Slot = item;
            ItemSlots.Add(slot);
        }
    }
    
    void ActivateEquippable(Item item)
    {
        var equippable = item.GetComponent<Equipable>();
        if(equippable != null)
        {
            var unit = GetComponentInParent<Unit>();
            equippable.Use(unit);
        }
    }
}
