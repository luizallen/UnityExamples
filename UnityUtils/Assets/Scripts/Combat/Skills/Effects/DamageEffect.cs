using Assets.Scripts.Combat;
using UnityEngine;

public class DamageEffect : SkillEffects
{
    [Header("Not in %")]
    public float BaseDamageMultiplier = 1;
    public float Randomness = 0.2f;
    public float GotHitDelay = 0.1f;

    public DamageType DamageType;
    public ElementalType ElementalType; 

    public override void Apply(Unit target)
    {
        var damage = Predict(target);
        var currentHp = target.GetStat(StatEnum.HP);

        var roll = Random.Range(1 - Randomness, 1 + Randomness);
        var finalDamage = (int)(damage * roll);

        target.SetStat(StatEnum.HP, -finalDamage);

        CombatLog.Append("{0} estava com {1} de HP, foi afetado por {2} * {3} = {4} e ficou com {5}",
                   target.name,
                   currentHp,
                   damage,
                   roll,
                   finalDamage,
                   target.GetStat(StatEnum.HP));

        if (target.Dead)
        {
            if (target.Active)
                target.AnimationController.Death(GotHitDelay);
            target.Active = false;
        }
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
            case DamageType.Physical:
                attackerScore += Turn.Unit.GetStat(StatEnum.ATK);
                defenderScore += target.GetStat(StatEnum.DEF);
                break;
            case DamageType.Magical:
                attackerScore += Turn.Unit.GetStat(StatEnum.MATK);
                defenderScore += target.GetStat(StatEnum.MDEF);
                break;
            default:
                break;
        }

        var attackerFinal = GetBonus(Turn.Unit, target, attackerScore); ;
        var defenderFinal = GetBonus(target, Turn.Unit, defenderScore);

        float calculation = (attackerFinal - (defenderFinal / 2)) * BaseDamageMultiplier;
        calculation = Mathf.Clamp(calculation, 0, calculation);
        return (int)calculation;
    }

    float GetBonus(Unit thisUnit, Unit otherUnit, float initialScore)
    {
        if(thisUnit.Stats.MultiplicativeModifier == null)
            return initialScore;
        

        var forms = new MultiplicativeForms();
        forms.OriginalValue = (int)initialScore;
        forms.ThisUnit = thisUnit;
        forms.OtherUnit = otherUnit;
        forms.ElementalType = ElementalType;
        thisUnit.Stats.MultiplicativeModifier(forms);
        float bonus = forms.CurrentValue;
        float final = initialScore * (1 + (bonus / 100));

        Debug.LogFormat("Damage {0} * bonus: {1}, final = {2}", initialScore, bonus, final);

        return final;
    }
}

