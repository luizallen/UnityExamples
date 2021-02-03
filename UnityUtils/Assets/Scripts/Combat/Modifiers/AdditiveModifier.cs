public class AdditiveModifier : Modifier
{ 
    protected override void Modify(Stat stat)
    {
        stat.CurrentValue += (int)Value;
    }
}
