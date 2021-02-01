using UnityEngine;

public abstract class SkillEffects : MonoBehaviour
{
    public abstract int Predict(Unit target);

    public abstract void Apply(Unit target);
}

