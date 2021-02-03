using System;
using UnityEngine;

public class HealEffect : SkillEffects
{
    [Header("Not in %")]
    public float Randomness = 0.3f;
    public float BaseMultiplier = 1;

    public override void Apply(Unit target)
    {
        var initial = Predict(target);

        var currentHp = target.GetStat(StatEnum.HP);
        float roll = UnityEngine.Random.Range(1 - Randomness, 1 + Randomness);

        var finalHeal = (int)(initial * roll);
        target.SetStat(StatEnum.HP, finalHeal);

        CombatLog.Append("{0} estava com {1} de HP, foi afetado por {2} * {3} = {4} e ficou com {5}",
            target.name,
            currentHp,
            initial,
            roll,
            finalHeal,
            target.GetStat(StatEnum.HP));
    }

    public override int Predict(Unit target)
    {
        float attackerScore = 0;
        attackerScore += Turn.Unit.GetStat(StatEnum.MATK) * BaseMultiplier;

        //bonus

        return (int)attackerScore;
    }
}
