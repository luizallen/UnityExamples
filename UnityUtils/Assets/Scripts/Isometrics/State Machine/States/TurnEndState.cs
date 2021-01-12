using System.Collections;
using UnityEngine;

public class TurnEndState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(AddUnitDelay());
    }


    IEnumerator AddUnitDelay()
    {
        Turn.Unit.ChargeTime += 300;
        
        if (Turn.HasMoved)
            Turn.Unit.ChargeTime += 100;
        
        if(Turn.HasActed)
            Turn.Unit.ChargeTime += 100;

        Turn.Unit.ChargeTime -= Turn.Unit.GetStat(StatEnum.SPEED);

        Turn.HasActed = Turn.HasMoved = false;
        StateMachine.Units.Remove(Turn.Unit);
        StateMachine.Units.Add(Turn.Unit);

        yield return new WaitForSeconds(0.5f);

        StateMachine.ChangeTo<TurnBeginState>();
    }
}
