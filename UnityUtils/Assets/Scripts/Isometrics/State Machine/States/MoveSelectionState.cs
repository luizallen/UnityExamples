using Assets.Scripts.Isometrics.State_Machine.States;
using System.Collections.Generic;

public class MoveSelectionState : State
{
    List<TileLogic> _tiles;

    public override void Enter()
    {
        base.Enter();
        Inputs.OnMove += OnMoveTileSelector;
        Inputs.OnFire += OnFire;
        _tiles = Board.Instance.Search(Turn.Unit.Tile);
        _tiles.Remove(Turn.Unit.Tile);
        Board.Instance.SelectTiles(_tiles, Turn.Unit.Alliance);
    }

    public override void Exit()
    {
        base.Exit();
        Inputs.OnMove -= OnMoveTileSelector;
        Inputs.OnFire -= OnFire;
        Board.Instance.DeSelectTiles(_tiles, Turn.Unit.Alliance);
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
}
