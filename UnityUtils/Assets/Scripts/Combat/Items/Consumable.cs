using UnityEngine;

public class Consumable : Item
{
    public Skill Skill;
    public override void Use(Unit unit)
    {
        Debug.Log("Consumiu");
    }
}
