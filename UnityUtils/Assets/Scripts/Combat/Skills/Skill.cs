using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int ManaCost;
    public Sprite Icon;

    Transform primary;

    Transform _primary
    {
        get
        {
            if(primary == null)
            {
                primary = transform.Find("Primary");
                _secondary = transform.Find("Secondary");
            }

            return primary;
        }
    }

    Transform _secondary;

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

    public int GetHitPrediction(Unit target) => _primary.GetComponentInChildren<HitRate>().Predict(target);

    public int GetHitPrediction(Unit target, Transform effect) => effect.GetComponentInChildren<HitRate>().Predict(target);

    public int GetEffectPrediction(Unit target) => _primary.GetComponentInChildren<SkillEffects>().Predict(target);

    public int GetEffectPrediction(Unit target, Transform effect) => effect.GetComponentInChildren<SkillEffects>().Predict(target);

    public void Perform()
    {
        FilterContent();
        SFX();

        foreach (var target in Turn.Targets)
        {
            var unit = target.content.GetComponent<Unit>();
            if (unit == null)
                continue;

            var didHit = RollToHit(unit, _primary);
            VFX(unit, didHit);

            if(unit!=null && didHit)
            {          
                GetComponentInChildren<SkillEffects>().Apply(unit);

                if (_secondary.childCount != 0 && RollToHit(unit, _secondary))
                    _secondary.GetComponentInChildren<SkillEffects>().Apply(unit);
            }
        }
    }

    void FilterContent() => Turn.Targets.RemoveAll(t => t.content == null);

    bool RollToHit(Unit unit, Transform effect)
    {
        var hit = effect.GetComponentInChildren<HitRate>().TryToHit(unit);
        if (hit)
        {
            CombatLog.Append("Acertou");
            return true;
        }

        CombatLog.Append("Errou");
        return false;
    }

    void VFX(Unit target, bool didHit)
    {
        var fx = GetComponentInChildren<SkillVisualFX>();
        if (fx != null)
        {
            fx.Target = target;
            fx.DidHit = didHit;
            fx.VFX(target);
        }
            
    }

    private void SFX()
    {
        var fx = GetComponentInChildren<SkillSoundFX>();
        if (fx != null)
            fx.Play();
    }
}
