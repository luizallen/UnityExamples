public class ModConditionJob : ModifierCondition
{
    public string JobName;

    public override bool Validate(object args)
    {
        var forms = (MultiplicativeForms)args;

        if (forms.OtherUnit.Job.name == JobName)
            return true;

        return base.Validate(args);
    }
}
