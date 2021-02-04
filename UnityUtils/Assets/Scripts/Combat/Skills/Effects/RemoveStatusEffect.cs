using System.Collections.Generic;
using UnityEngine;

public class RemoveStatusEffect : SkillEffects
{
    public List<string> StatusNames;

    public override void Apply(Unit target)
    {
        var holder = target.transform.Find("Status");

        foreach (var name in StatusNames)
        {
            SeekAndDestroy(name, holder);
        }
    }

    public override int Predict(Unit target) => 0;

    void SeekAndDestroy(string name, Transform holder)
    {
        var status = holder.Find(name);

        if (status != null)
            Destroy(status.gameObject);
    }
}
