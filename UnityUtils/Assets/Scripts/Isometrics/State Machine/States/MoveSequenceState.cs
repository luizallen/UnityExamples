using Assets.Scripts.Isometrics.State_Machine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequenceState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(MoveSequence());
    }

    IEnumerator MoveSequence()
    {
        List<TileLogic> path = new List<TileLogic>();
        path.Add(StateMachine.SelectedTile);

        var movement = Turn.Unit.GetComponent<Movement>();
        yield return StartCoroutine(movement.Move(path));

        Turn.Unit.Tile = StateMachine.SelectedTile; 
        yield return null;
        CombatLog.Append(string.Format("O {0} se moveu para {1}", Turn.Unit.name, StateMachine.SelectedTile.content.transform.position));

        yield return new WaitForSeconds(0.2f);
        Turn.HasMoved = true;
        StateMachine.ChangeTo<ChooseActionState>();
    }
}
