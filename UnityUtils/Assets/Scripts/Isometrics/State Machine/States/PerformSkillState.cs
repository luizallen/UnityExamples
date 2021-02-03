using System.Collections;
using UnityEngine;

public class PerformSkillState : State
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

        StateMachine.ChangeTo<TurnEndState>();
    }
}
