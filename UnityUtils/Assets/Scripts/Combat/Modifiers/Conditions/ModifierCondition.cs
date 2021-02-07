using UnityEngine;

public abstract class ModifierCondition : ScriptableObject
{
    public virtual bool Validate(object args) => false;

    public virtual bool Validate(object args1, object args2) => false;
}
