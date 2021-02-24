using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTargetState : CombatState
{
    List<TileLogic> _selectedTiles;
    bool _directionOriented;

    public override void Enter()
    {
        base.Enter();

        _selectedTiles = Turn.Skill.GetTargets();
        Board.SelectTiles(_selectedTiles, Turn.Unit.Alliance);

        _directionOriented = Turn.Skill.GetComponentInChildren<SkillRange>().IsDirectionOriented();

        if (Turn.Unit.PlayerType == PlayerType.Human)
        {
            if (_directionOriented)
                Inputs.OnMove += ChangeDirection;
            else
                Inputs.OnMove += OnMoveTileSelector;

            Inputs.OnFire += OnFire;
        }
        else
            StartCoroutine(ComputerTargeting());
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

    IEnumerator ComputerTargeting()
    {
        var skillRange = Turn.Skill.GetComponentInChildren<SkillRange>();
        var plan = ComputerPlayer.Instance.CurrentPlan;

        if (skillRange.IsDirectionOriented())
        {
            switch (plan.Direction)
            {
                case 'N':
                    ChangeDirection(null, Vector3Int.up);
                    break;
                case 'S':
                    ChangeDirection(null, Vector3Int.down);
                    break;
                case 'E':
                    ChangeDirection(null, Vector3Int.right);
                    break;
                default: //W
                    ChangeDirection(null, Vector3Int.left);
                    break;
            }

            yield return new WaitForSeconds(0.25f);
        }
        else
            yield return StartCoroutine(AIMoveSelector(plan.SkillTargetPos));

        yield return new WaitForSeconds(1);
        Turn.Targets = _selectedTiles;
        StateMachine.ChangeTo<ConfirmSkillState>();
    }
}
