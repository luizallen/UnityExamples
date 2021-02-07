using System.Collections.Generic;
using UnityEngine;

public class SkillTargetState : State
{
    List<TileLogic> _selectedTiles;
    bool _directionOriented;

    public override void Enter()
    {
        base.Enter();
        _directionOriented = Turn.Skill.GetComponentInChildren<SkillRange>().IsDirectionOriented();

        if (_directionOriented)
            Inputs.OnMove += ChangeDirection;
        else
            Inputs.OnMove += OnMoveTileSelector;

        Inputs.OnFire += OnFire;

        _selectedTiles = Turn.Skill.GetTargets();
        Board.SelectTiles(_selectedTiles, Turn.Unit.Alliance);
    }

    public override void Exit()
    {
        base.Exit();

        if (_directionOriented)
            Inputs.OnMove -= ChangeDirection;
        else
            Inputs.OnMove -= OnMoveTileSelector;

        Inputs.OnFire -= OnFire;

        Board.DeSelectTiles(_selectedTiles);
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;

        if (button == 1 && (_directionOriented || _selectedTiles.Contains(Selector.Instance.Tile)))
        {
            Turn.Targets = _selectedTiles;
            StateMachine.ChangeTo<ConfirmSkillState>();
        }
        else if (button == 2)
        {
            if (Turn.IsItem != null)
                StateMachine.ChangeTo<ItemSelectionState>();
            else
                StateMachine.ChangeTo<SkillSelectionState>();
        }
    }

    void ChangeDirection(object sender, object args)
    {
        var pos = (Vector3Int)args;
        var dir = Turn.Unit.Tile.GetDirection(Turn.Unit.Tile.Pos + pos);

        if (Turn.Unit.Direction != dir)
        {
            Board.DeSelectTiles(_selectedTiles);
            Turn.Unit.Direction = dir;
            Turn.Unit.GetComponent<AnimationController>().Idle();

            _selectedTiles = Turn.Skill.GetTargets();
            Board.SelectTiles(_selectedTiles, Turn.Unit.Alliance);
        }
    }
}
