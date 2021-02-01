using UnityEngine;

public class SkillAffects : MonoBehaviour
{
    public SKillAffectsType WhoItAffects;

    public bool IsTarget(Unit unit)
    {
        switch (WhoItAffects)
        {
            case SKillAffectsType.AllyOnly:
                return IsAlly(unit);
            case SKillAffectsType.EnemyOnly:
                return IsEnemy(unit);
            case SKillAffectsType.Default:
            default:
                return true;
        }
    }

    bool IsEnemy(Unit unit) => unit.Alliance != Turn.Unit.Alliance;

    bool IsAlly(Unit unit) => unit.Alliance == Turn.Unit.Alliance;
}

public enum SKillAffectsType
{
    Default,
    AllyOnly,
    EnemyOnly
}
