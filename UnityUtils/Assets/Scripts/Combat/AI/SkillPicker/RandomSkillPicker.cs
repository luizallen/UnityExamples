using System.Collections.Generic;
using UnityEngine;

public class RandomSkillPicker : SkillPicker
{
    public List<string> SkillsToPick;

    public override void Pick(AIPlan plan)
    {
        if (SkillsToPick != null && SkillsToPick.Count != 0)
            plan.Skill = Find(SkillsToPick[Random.Range(0, SkillsToPick.Count)]);
        else
            plan.Skill = Skills[Random.Range(0, Skills.Count)];

        plan.TargetType = plan.Skill.GetComponentInChildren<SkillAffects>().WhoItAffects;
    }
}
