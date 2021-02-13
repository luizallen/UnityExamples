using UnityEngine;

public class SkillAffects : MonoBehaviour
{
    public SkillAffectsType WhoItAffects;

    public bool IsTarget(Unit unit)
    {
        switch (WhoItAffects)
        {
            case SkillAffectsType.AllyOnly:
                return IsAlly(unit);
            case SkillAffectsType.EnemyOnly:
                return IsEnemy(unit);
            case SkillAffectsType.Default:
            default:
                return true;
        }
    }

    bool IsEnemy(Unit unit) => unit.Alliance != Turn.Unit.Alliance;

    bool IsAlly(Unit unit) => unit.Alliance == Turn.Unit.Alliance;
}

public enum SkillAffectsType
{
    Default,
    AllyOnly,
    EnemyOnly
}
