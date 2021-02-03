using UnityEngine;

public abstract class CombatStatus : MonoBehaviour
{
    [HideInInspector]
    public Unit Unit;

    [HideInInspector]
    public int Duration;

    public abstract void Effect();

    protected virtual void OnDisable()
    {
        var modifiers = GetComponents<Modifier>();

        foreach (var modifier in modifiers)
        {
            modifier.Deactivate();
        }
    }
}
