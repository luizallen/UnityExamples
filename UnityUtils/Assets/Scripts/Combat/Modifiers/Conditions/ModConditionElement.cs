public class ModConditionElement : ModifierCondition
{
    public ElementalType Type;

    public override bool Validate(object args)
    {
        var forms = (MultiplicativeForms)args;

        if (forms.ElementalType == Type)
            return true;
        else if(forms.ElementalType == ElementalType.Weapon)
        {
            var item = Turn.Unit.Equipment.GetItem(ItemSlot.MainHand);
            if (item == null)
                return false;

            var mod = item.GetComponent<ElementalModifier>();
            if (mod != null && mod.Type == Type)
                return true;
        }
        return false;
    }
}
