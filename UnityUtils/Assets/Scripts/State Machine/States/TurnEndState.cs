using System.Collections;
using UnityEngine;

public class TurnEndState : State
{
    public override void Enter()
    {
        base.Enter();

        CombatLog.CheckActive();

        if (CombatLog.IsOver())
            StateMachine.ChangeTo<MapEndState>();
        else
            StartCoroutine(AddUnitDelay());
    }


    IEnumerator AddUnitDelay()
    {
        Turn.Unit.ChargeTime += 300;

        if (Turn.HasMoved)
            Turn.Unit.ChargeTime += 100;

        if (Turn.HasActed)
            Turn.Unit.ChargeTime += 100;

        Turn.Unit.ChargeTime -= Turn.Unit.GetStat(StatEnum.SPEED);

        Turn.HasActed = Turn.HasMoved = false;
        Turn.Skill = null;
        Turn.IsItem = null;
        ComputerPlayer.Instance.CurrentPlan = null;


        StateMachine.Units.Remove(Turn.Unit);
        StateMachine.Units.Add(Turn.Unit);

        CombatLog.Append(string.Format("O {0} terminou seu turno", Turn.Unit.name));

        yield return new WaitForSeconds(0.5f);
        StateMachine.ChangeTo<TurnBeginState>();
    }
}
