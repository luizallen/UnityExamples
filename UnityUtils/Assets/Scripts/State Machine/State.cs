﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    protected InputController Inputs { get { return InputController.Instance; } }

    protected StateMachineController StateMachine { get { return StateMachineController.Instance; } }

    protected Board Board { get { return Board.Instance; } }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    protected void OnMoveTileSelector(object sender, object args)
    {
        Vector3Int input = (Vector3Int)args;
        TileLogic tile = Board.GetTile(Selector.Instance.Position + input);

        if (tile != null)
        {
            MoveSelector(tile);
        }
    }

    protected void MoveSelector(Vector3Int pos)
    {
        MoveSelector(Board.GetTile(pos));
    }

    protected void MoveSelector(TileLogic tile)
    {
        Selector.Instance.Tile = tile;
        Selector.Instance.SpriteRenderer.sortingOrder = tile.ContentOrder;
        Selector.Instance.transform.position = tile.WorldPos;
        StateMachine.SelectedTile = tile;
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