using UnityEngine;

public class InflictStatusEffect : SkillEffects
{
    public CombatStatus Status;
    public string StatusName;
    public int StatusValue;
    public int Duration;


    public override void Apply(Unit target)
    {
        var holder = target.transform.Find("Status");
        var stack = holder.Find(StatusName);

        if (stack != null)
            Stack(stack);
        else
            CreateNew(holder, target);
    }

    public override int Predict(Unit target) => 0;

    void CreateNew(Transform holder, Unit target)
    {
        var instantiatedStatus = Instantiate(Status, holder.position, Quaternion.identity, holder);
        instantiatedStatus.name = StatusName;
        instantiatedStatus.SetModifiers(StatusValue);
        instantiatedStatus.Unit = target;
        instantiatedStatus.Duration = Duration;
        instantiatedStatus.Effect();
    }

    void Stack(Transform stack)
    {
        var stackStatus = stack.GetComponent<CombatStatus>();
        stackStatus.Stack(Duration, StatusValue);
    }
}
