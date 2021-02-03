public class PermanentStatus : CombatStatus
{
    public override void Effect()
    {
        var modifiers = GetComponents<Modifier>();

        foreach (var modifier in modifiers)
        {
            modifier.Activate(Unit);
        }
    }
}
