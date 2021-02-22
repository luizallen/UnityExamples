using Assets.Scripts.Isometrics.State_Machine.States;
using UnityEngine;

public class RoamState : State
{
    public override void Enter()
    {
        base.Enter();
        Inputs.OnMove += OnMoveTileSelector;
        Inputs.OnFire += OnFire;
        CheckNullPosition();
    }

    public override void Exit()
    {
        base.Exit();
        Inputs.OnMove -= OnMoveTileSelector;
        Inputs.OnFire -= OnFire;
    }

    void CheckNullPosition()
    {
        if (Selector.Instance.Tile == null)
        {
            TileLogic t = Board.GetTile(new Vector3Int(1, -5, 0));
            Selector.Instance.Tile = t;
            Selector.Instance.SpriteRenderer.sortingOrder = t.ContentOrder;
            Selector.Instance.transform.position = t.WorldPos;
        }
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;

        if (button == 1)
        {

        }
        else if (button == 2)
        {
            StateMachine.ChangeTo<ChooseActionState>();
        }
    }
}
