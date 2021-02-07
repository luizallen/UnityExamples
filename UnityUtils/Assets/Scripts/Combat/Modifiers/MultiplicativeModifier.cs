public class MultiplicativeModifier : Modifier
{
    public MultiplicativeType Type;

    public ModifierCondition Condition;

    protected override void Modify(object args)
    {
        var forms = (MultiplicativeForms)args;

        if(TypeCheck() && (Condition == null || Condition.Validate(forms)))
            forms.CurrentValue += (int)Value;
    }
    public override void Activate(Unit unit)
    {
        base.Activate(unit);

        _host.Stats.MultiplicativeModifier += Modify;
    }
    public override void Deactivate()
    {
        _host.Stats.MultiplicativeModifier -= Modify;
    }

    bool TypeCheck()
    {
        if (Type == MultiplicativeType.Attacker)
        {
            if (Turn.Unit == _host)
                return true;
        }
        else
        {
            if (Turn.Targets.Contains(_host.Tile))
                return true;
        }

        return false;
    }
}

public enum MultiplicativeType
{
    Attacker,
    Defender
}
