using System.Collections.Generic;
using UnityEngine;

public abstract class SkillPicker : MonoBehaviour
{
    public const string DefaultSkill = "NormalAttack";

    protected List<Skill> Skills { get => Turn.Unit.Job.Skills; }

    public abstract void Pick(AIPlan plan);

    protected List<Skill> Find(SkillAffectsType type)
    {
        var result = new List<Skill>();
        foreach (var skill in Skills)
        {
            if (skill.GetComponentInChildren<SkillAffects>().WhoItAffects == type)
                result.Add(skill);
        }

        if (result.Count == 0)
            result.Add(Default());

        return result;
    }

    protected Skill Find(string skillName) => Skills.Find(x => x.name == skillName) ?? Default();

    protected Skill Default() => Skills.Find(x => x.name == DefaultSkill);
}
