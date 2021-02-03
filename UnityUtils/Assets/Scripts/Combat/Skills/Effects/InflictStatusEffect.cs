using UnityEngine;

public class InflictStatusEffect : SkillEffects
{
    public int Duration;
    public CombatStatus Status;

    public override void Apply(Unit target)
    {
        var holder = target.transform.Find("Status");

        var instantiatedStatus = Instantiate(Status, holder.position, Quaternion.identity, holder);
        instantiatedStatus.name = instantiatedStatus.name.Replace("(Clone)", "");
        instantiatedStatus.Unit = target;
        instantiatedStatus.Duration = Duration;
        instantiatedStatus.Effect();
    }

    public override int Predict(Unit target) => 0;

}
