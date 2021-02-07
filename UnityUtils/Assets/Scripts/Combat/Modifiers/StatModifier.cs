public class StatModifier : Modifier
{
    public StatEnum Stat;

    protected override void Modify(object args)
    {
        var stat = (Stat)args;
        stat.CurrentValue += (int)Value;
    }

    public override void Activate(Unit unit)
    {
        base.Activate(unit);
        _host.Stats[Stat].StatModifiers += Modify;
        _host.UpdateStat(Stat);
    }
    public override void Deactivate()
    {
        _host.Stats[Stat].StatModifiers -= Modify;
        _host.UpdateStat(Stat);
    }
}
