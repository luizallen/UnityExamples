using System.Collections;
using UnityEngine;

public class PerformSkillState : CombatState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(PerformSequence());
    }

    IEnumerator PerformSequence()
    {
        yield return null;

        Turn.Unit.Direction = Turn.Skill.GetComponentInChildren<SkillRange>().GetDirection();
        Turn.Unit.AnimationController.Idle();
        Turn.Unit.AnimationController.Attack();

        Turn.Skill.Perform();
        yield return new WaitForSeconds(1f);

        if (Turn.IsItem != null)
            Turn.Unit.Equipment.UnEquip(Turn.IsItem);

        StateMachine.ChangeTo<TurnEndState>();
    }
}
