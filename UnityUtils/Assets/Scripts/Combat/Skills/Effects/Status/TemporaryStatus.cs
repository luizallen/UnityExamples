public class TemporaryStatus : CombatStatus
{
    protected override void OnDisable()
    {
        base.OnDisable();
        Unit.OnTurnBegin -= DurationCounter;
    }

    public override void Effect()
    {
        Unit.OnTurnBegin += DurationCounter;
        var modifiers = GetComponents<Modifier>();

        foreach (var modifier in modifiers)
        {
            modifier.Activate(Unit);
        }
    }

    void DurationCounter (){
        Duration--;
        if (Duration <= 0)
            Destroy(this.gameObject);
    }
}
