using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Job : ScriptableObject
{
    public List<Stat> Stats;
    public List<Skill> Skills;
    public string SpriteModel;
    public int AdvancesAtLevel;
    public List<Job> AdvancesTo;
    public Sprite Portrait;

    [TextArea]
    public string Description;

    public void InitStats()
    {
        Stats = new List<Stat>();

        foreach (StatEnum item in Enum.GetValues(typeof(StatEnum)))
        {
            Stats.Add(new Stat { Type = item });
        }
    }

    public static void LevelUp(Unit unit, int amount)
    {
        var toLevelStats = unit.Stats;

        foreach (var stat in toLevelStats.StatsList)
        {
            stat.BaseValue += Mathf.FloorToInt(stat.Growth * amount);
        }

        toLevelStats[StatEnum.LVL].BaseValue += amount;
        toLevelStats[StatEnum.HP].BaseValue = toLevelStats[StatEnum.MAXHP].BaseValue;
        toLevelStats[StatEnum.MP].BaseValue = toLevelStats[StatEnum.MAXMP].BaseValue;

        unit.UpdateStat();
    }

    public static int GetExpCurveValue(int level)
    {
        var value = 0;
        for(var i = 1; i <level; i++)
        {
            value += i * 1000;
        }
        return value;
    }

    public static void CheckLevelUp(Unit unit)
    {
        var required = GetExpCurveValue(unit.Stats[StatEnum.LVL].BaseValue + 1);
        if (unit.Experience >= required)
            LevelUp(unit, 1);
    }

    public static bool CanAdvance(Unit unit) => unit.GetStat(StatEnum.LVL) >= unit.Job.AdvancesAtLevel;


    public static void Employ(Unit unit, Job job, int level)
    {
        unit.Job = job;
        unit.SpriteModel = job.SpriteModel;
        SetStats(unit.Stats, job);

        unit.UpdateStat();

        Job.LevelUp(unit, level - 1);

        var skillBook = unit.GetComponentInChildren<SkillBook>();
        skillBook.Skills = new List<Skill>();
        skillBook.Skills.AddRange(job.Skills);
    }

    public static void SetStats(Stats stats, Job job)
    {
        stats.StatsList = job.Stats.Select(s =>
            new Stat
            {
                BaseValue = s.BaseValue,
                CurrentValue = s.CurrentValue,
                Growth = s.Growth,
                Type = s.Type
            })
            .ToList();


        stats[StatEnum.HP].BaseValue = stats[StatEnum.MAXHP].BaseValue;
        stats[StatEnum.MP].BaseValue = stats[StatEnum.MAXMP].BaseValue;
    }
}
