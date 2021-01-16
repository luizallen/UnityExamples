using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int Damage;
    public int ManaCost;
    public Sprite Icon;

    public bool CanUse()
    {
        if (Turn.Unit.GetStat(StatEnum.MP) >= ManaCost)
            return true;

        return false;
    }

    public bool ValidadeTarget()
    {
        Unit unit = null;

        if (StateMachineController.Instance.SelectedTile != null)
            unit = StateMachineController.Instance.SelectedTile.content?.GetComponent<Unit>();

        if (unit == null)
            return false;

        return true;
    }

    public List<TileLogic> GetTargets()
    {
        var targets = new List<TileLogic>();

        targets.Add(StateMachineController.Instance.SelectedTile);

        return targets;
    }

    public virtual void Effect() { }

    protected void FilterContent() => Turn.Targets.RemoveAll(t => t.content == null);
}
