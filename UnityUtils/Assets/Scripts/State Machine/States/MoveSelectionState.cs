using Assets.Scripts.Isometrics.State_Machine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectionState : State
{
    List<TileLogic> _tiles;

    public override void Enter()
    {
        base.Enter();
       
        _tiles = Board.Instance.Search(Turn.Unit.Tile, Turn.Unit.GetComponent<Movement>().ValidateMovement);
        _tiles.Remove(Turn.Unit.Tile);
        Board.Instance.SelectTiles(_tiles, Turn.Unit.Alliance);

        if (Turn.Unit.PlayerType == PlayerType.Human)
        {
            Inputs.OnMove += OnMoveTileSelector;
            Inputs.OnFire += OnFire;
        }
        else
            StartCoroutine(ComputerSelectMoveTarget());
    }

    public override void Exit()
    {
        base.Exit();
        Inputs.OnMove -= OnMoveTileSelector;
        Inputs.OnFire -= OnFire;
        Board.Instance.DeSelectTiles(_tiles);
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;

        if (button == 1)
        {
            if(_tiles.Contains(StateMachine.SelectedTile))
                StateMachine.ChangeTo<MoveSequenceState>();
        }
        else if (button == 2)
        {
            StateMachine.ChangeTo<ChooseActionState>();
        }
    }

    IEnumerator ComputerSelectMoveTarget()
    {
        var plan = ComputerPlayer.Instance.CurrentPlan;
        yield return StartCoroutine(AIMoveSelector(plan.MovePos));

        yield return new WaitForSeconds(0.5f);
        StateMachine.ChangeTo<MoveSequenceState>();
    }

}
