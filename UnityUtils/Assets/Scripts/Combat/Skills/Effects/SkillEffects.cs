using Assets.Scripts.Combat;
using UnityEngine;

public abstract class SkillEffects : MonoBehaviour
{
    public DamageType DamageType;

    public abstract int Predict(Unit target);

    public abstract void Apply(Unit target);
}

