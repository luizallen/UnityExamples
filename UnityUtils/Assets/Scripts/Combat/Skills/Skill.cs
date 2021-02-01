using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int Damage;
    public int ManaCost;
    public Sprite Icon;
    public float GotHitAnimationDelay;

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
            if (tileLogic != null && tileLogic.content?.GetComponent<Unit>() != null)
                return true;
        }

        return false;
    }

    public List<TileLogic> GetTargets() => GetComponentInChildren<SkillRange>().GetTileInRange();

    public List<TileLogic> GetArea() => GetComponentInChildren<AreaOfEffect>().GetArea(Turn.Targets);

    public void Effect() {
        FilterContent();

        for (int i = 0; i < Turn.Targets.Count; i++)
        {
            var unit = Turn.Targets[i].content.GetComponent<Unit>();

            if (unit != null)
            {
                var hp = unit.GetStat(StatEnum.HP);

                CombatLog.Append(string.Format("{0} estava com {1} HP, foi afetado por {2} e ficou com {3}", unit, hp, -Damage, hp - Damage));
                unit.SetStat(StatEnum.HP, -Damage);

                if (unit.Dead)
                    unit.AnimationController.Death(GotHitAnimationDelay);
                else
                {
                    unit.AnimationController.Idle();
                    unit.AnimationController.GotHit(GotHitAnimationDelay);
                }
            }
        }
    }

    protected void FilterContent() => Turn.Targets.RemoveAll(t => t.content == null);
}
