using UnityEngine;

public class DamageEffect : SkillEffects
{
    public DameType DamageType;

    [Header("Not in %")]
    public float BaseDamageMultiplier = 1;
    public float Randomness = 0.2f;
    public float GotHitDelay = 0.1f;

    public override void Apply(Unit target)
    {
        var damage = Predict(target);
        var currentHp = target.GetStat(StatEnum.HP);

        var roll = Random.Range(1 - Randomness, 1 + Randomness);
        var finalDamage = (int)(damage * roll);

        target.SetStat(StatEnum.HP, -Mathf.Clamp(finalDamage, 0, currentHp));

        CombatLog.Append("{0} estava com {1} de HP, foi afetado por {2} * {3} = {4} e ficou com {5}",
                   target.name,
                   currentHp,
                   damage,
                   roll,
                   finalDamage,
                   target.GetStat(StatEnum.HP));

        if (target.Dead)
            target.AnimationController.Death(GotHitDelay);
        else
        {
            target.AnimationController.Idle();
            target.AnimationController.GotHit(GotHitDelay);
        }
    }

    public override int Predict(Unit target)
    {
        float attackerScore = 0;
        float defenderScore = 0;

        switch (DamageType)
        {
            case DameType.Physical:
                attackerScore += Turn.Unit.GetStat(StatEnum.ATK);
                defenderScore += target.GetStat(StatEnum.DEF);
                //bonus
                break;
            case DameType.Magical:
                attackerScore += Turn.Unit.GetStat(StatEnum.MATK);
                defenderScore += target.GetStat(StatEnum.MDEF);
                //bonus
                break;
            default:
                break;
        }

        float calculation = (attackerScore - (defenderScore / 2)) * BaseDamageMultiplier;
        calculation = Mathf.Clamp(calculation, 0, calculation);
        return (int)calculation;
    }
}

public enum DameType
{
    Physical,
    Magical,
}

