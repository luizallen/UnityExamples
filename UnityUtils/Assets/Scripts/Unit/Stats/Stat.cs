[System.Serializable]
public class Stat
{
    public StatEnum Type;
    public int BaseValue;
    public int CurrentValue;
    public float Growth;
    public StatModifierHandler StatModifiers;
}

public delegate void StatModifierHandler(Stat stat);
