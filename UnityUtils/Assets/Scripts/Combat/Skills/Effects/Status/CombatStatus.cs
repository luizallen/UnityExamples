using Assets.Scripts.Combat;
using UnityEngine;

public abstract class CombatStatus : MonoBehaviour
{
    [HideInInspector]
    public Unit Unit;
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

    public void SetModifiers(int value)
    {
        foreach (var modifier in GetComponents<Modifier>())
        {
            modifier.Value = value;
        }
    }

    public void Stack(int rcvDuration, int value)
    {
        Duration += rcvDuration;

        foreach (var modifier in GetComponents<Modifier>())
        {
            modifier.Value += value;
        }
    }
}
