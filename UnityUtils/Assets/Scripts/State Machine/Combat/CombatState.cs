﻿using System.Collections;
using UnityEngine;

public class CombatState : State
{
    protected CombatStateMachineController StateMachine { get { return CombatStateMachineController.Instance; } }

    protected Board Board { get { return Board.Instance; } }

    protected void OnMoveTileSelector(object sender, object args)
    {
        Vector3Int input = (Vector3Int)args;
        TileLogic tile = Board.GetTile(Selector.Instance.Position + input);

        if (tile != null)
        {
            MoveSelector(tile, StateMachine);
        }
    }

    protected IEnumerator AIMoveSelector(Vector3Int destination)
    {
        while (Selector.Instance.Position != destination)
        {
            if (Selector.Instance.Position.x < destination.x)
                OnMoveTileSelector(null, Vector3Int.right);

            if (Selector.Instance.Position.x > destination.x)
                OnMoveTileSelector(null, Vector3Int.left);

            if (Selector.Instance.Position.y < destination.y)
                OnMoveTileSelector(null, Vector3Int.up);

            if (Selector.Instance.Position.y > destination.y)
                OnMoveTileSelector(null, Vector3Int.down);

            yield return new WaitForSeconds(0.25f);
        }
    }
}
