using Assets.Scripts.Isometrics.State_Machine.States;
using System.Collections;

public class TurnBeginState : CombatState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(SelectUnit());
    }

    IEnumerator SelectUnit()
    {
        BreakDraw();
        StateMachine.Units.Sort((x, y) => x.ChargeTime.CompareTo(y.ChargeTime));
        Turn.Unit = StateMachine.Units[0];

        yield return null;
        Turn.Unit.OnTurnBegin?.Invoke();

        yield return null;
        if (Turn.Unit.Dead)
        {
            if (Turn.Unit.Active)
                Turn.Unit.AnimationController.Death();

            Turn.Unit.Active = false;
            StateMachine.ChangeTo<TurnEndState>();
        }
        else
        {
            if (Job.CanAdvance(Turn.Unit))
                StateMachine.ChangeTo<JobAdvanceState>();
            else
                StateMachine.ChangeTo<ChooseActionState>();
        }
    }

    void BreakDraw()
    {
        for (int i = 0; i < StateMachine.Units.Count - 1; i++)
        {
            if (StateMachine.Units[i].ChargeTime == StateMachine.Units[i + 1].ChargeTime)
                StateMachine.Units[i + 1].ChargeTime += 1;
        }
    }
}
