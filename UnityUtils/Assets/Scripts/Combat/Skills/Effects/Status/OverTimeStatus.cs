public class OverTimeStatus : CombatStatus
{
    public override void Effect()
    {
        Unit.OnTurnBegin += OverTimeEffect;
    }

    protected override void OnDisable()
    {
        Unit.OnTurnBegin -= OverTimeEffect;
    }

    void OverTimeEffect()
    {
        Duration--;
        var modifiers = GetComponents<StatModifier>();
        foreach (var modifier in modifiers)
        {
            Unit.SetStat(modifier.Stat, (int)modifier.Value);
        }

        if (Duration <= 0)
            Destroy(gameObject);
    }
}
