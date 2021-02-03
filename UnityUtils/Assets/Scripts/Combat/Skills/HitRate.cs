using UnityEngine;

public class HitRate : MonoBehaviour
{
    public HitRateType Type;
    public float BaseBonusChance;

    public int Predict(Unit target)
    {
        var hitScore = 0;
        var missScore = 0;

        switch (Type)
        {
            case HitRateType.Full:
                return 100;
            case HitRateType.Attack:
                hitScore = Turn.Unit.GetStat(StatEnum.ACC);
                missScore = target.GetStat(StatEnum.EVD);
                //Outros Bonus
                break;
            case HitRateType.InflictStatus:
            default:
                hitScore = Turn.Unit.GetStat(StatEnum.ACC);
                missScore = target.GetStat(StatEnum.RES);
                //Outros Bonus
                break;
        }

        return 75 - (missScore - hitScore); //Pode mudar a media para 75 para ter a chance maior de acerto
    }

    public bool TryToHit(Unit target)
    {
        float chance = Predict(target);

        float roll = Random.Range(0, 100 - BaseBonusChance);

        CombatLog.Append("Base chance from stats is {0}. Rolled for {1} + {2} from BonusChance", chance, roll, BaseBonusChance);
        chance += roll + BaseBonusChance;

        return chance >= 100;
    }
}

public enum HitRateType
{
    Attack,
    InflictStatus,
    Full
}