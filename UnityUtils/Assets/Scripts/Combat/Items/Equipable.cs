public class Equipable : Item
{
    public override void Use(Unit unit)
    {
        foreach (var modifier in GetComponents<Modifier>())
        {
            modifier.Activate(unit);
        }
    }
}
