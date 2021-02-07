using UnityEngine;

public abstract class Modifier : MonoBehaviour
{
    public float Value;

    protected Unit _host;

    public virtual void Activate(Unit unit) {
        _host = unit;
    }

    public abstract void Deactivate();

    protected abstract void Modify(object args);
}
