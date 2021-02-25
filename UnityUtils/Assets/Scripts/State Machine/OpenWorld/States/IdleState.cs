using UnityEngine;

public class IdleState : OpenWorldState
{
    public override void Enter()
    {
        Inputs.OnMove += OnMove;
        base.Enter();
    }

    public override void Exit()
    {
        Inputs.OnMove -= OnMove;
        base.Exit();
    }

    void OnMove(object sender, object args)
    {
        var button = (Vector3Int)args;

        if (button == Vector3Int.left)
        {
        }
        else if (button == Vector3Int.right)
        {
        }
    }
}
