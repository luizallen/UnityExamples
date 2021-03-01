using Assets.Scripts.Isometrics.State_Machine.States;
using System.Collections;
using UnityEngine;

public class MoveSequenceState : CombatState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(MoveSequence());
    }

    IEnumerator MoveSequence()
    {
        var movement = Turn.Unit.GetComponent<Movement>();
        yield return StartCoroutine(movement.Move(Turn.Unit.Tile, StateMachine.SelectedTile, Turn.Unit));

        Turn.Unit.Tile.content = null;
        Turn.Unit.Tile = StateMachine.SelectedTile;
        Turn.Unit.Tile.content = Turn.Unit.gameObject;

        yield return null;
        CombatLog.Append(string.Format("O {0} se moveu para {1}", Turn.Unit.name, StateMachine.SelectedTile.content.transform.position));

        yield return new WaitForSeconds(0.2f);
        Turn.HasMoved = true;
        StateMachine.ChangeTo<ChooseActionState>();
    }
}
