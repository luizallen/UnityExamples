public class FixedSkillPicker : SkillPicker
{
    public string SkillName;

    public override void Pick(AIPlan plan)
    {
        plan.Skill = Find(SkillName);
        plan.TargetType = plan.Skill.GetComponentInChildren<SkillAffects>().WhoItAffects;
    }
}
