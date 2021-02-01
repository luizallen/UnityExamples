using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int ManaCost;
    public Sprite Icon;

    public bool CanUse()
    {
        if (Turn.Unit.GetStat(StatEnum.MP) >= ManaCost)
            return true;

        return false;
    }

    public bool ValidadeTarget(List<TileLogic> targets)
    {
        foreach (var tileLogic in targets)
        {
            if (tileLogic != null)
            {
                var unit = tileLogic.content?.GetComponent<Unit>();

                if (unit != null && GetComponentInChildren<SkillAffects>().IsTarget(unit))
                    return true;
            }
        }

        return false;
    }

    public List<TileLogic> GetTargets() => GetComponentInChildren<SkillRange>().GetTileInRange();

    public List<TileLogic> GetArea() => GetComponentInChildren<AreaOfEffect>().GetArea(Turn.Targets);

    public int GetHitPrediction(Unit target) => GetComponentInChildren<HitRate>().Predict(target);

    public int GetDamagePrediction(Unit target) => GetComponentInChildren<SkillEffects>().Predict(target);

    public void Perform()
    {
        FilterContent();

        foreach (var target in Turn.Targets)
        {
            var unit = target.content.GetComponent<Unit>();

            if(unit!=null && RollToHit(unit))
                GetComponentInChildren<SkillEffects>().Apply(unit);
        }
    }

    void FilterContent() => Turn.Targets.RemoveAll(t => t.content == null);

    bool RollToHit(Unit unit)
    {
        var hit = GetComponentInChildren<HitRate>().TryToHit(unit);
        if (hit)
        {
            CombatLog.Append("Acertou");
            return true;
        }

        CombatLog.Append("Errou");
        return false;
    }
}
