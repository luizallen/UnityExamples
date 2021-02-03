using UnityEngine;

public abstract class Modifier : MonoBehaviour
{
    public StatEnum Stat;
    public float Value;

    Unit _host;

    public virtual void Activate(Unit unit)
    {
        _host = unit;
        _host.Stats[Stat].Modifiers += Modify;
        _host.UpdateStat(Stat);
    }

    public virtual void Deactivate()
    {
        _host.Stats[Stat].Modifiers -= Modify;
        _host.UpdateStat(Stat);
    }

    protected abstract void Modify(Stat stat);
}
