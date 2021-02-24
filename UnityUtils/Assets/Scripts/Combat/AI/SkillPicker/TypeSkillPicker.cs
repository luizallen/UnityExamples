using UnityEngine;

public class TypeSkillPicker : SkillPicker
{
    public SkillAffectsType Type;

    public override void Pick(AIPlan plan)
    {
        var toPick = Find(Type);

        plan.Skill = toPick[Random.Range(0, toPick.Count)];
        plan.TargetType = Type;
    }
}
