[System.Serializable]
public class Stat
{
    public StatEnum Type;
    public int BaseValue;
    public int CurrentValue;
    public float Growth;
    public StatModifier Modifiers;
}

public delegate void StatModifier(Stat stat);
