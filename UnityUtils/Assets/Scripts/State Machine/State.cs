using UnityEngine;

public class State : MonoBehaviour
{
    protected InputController Inputs { get { return InputController.Instance; } }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    protected void MoveSelector(Vector3Int pos, BaseStateMachine stateMachine)
    {
        var tile = Board.GetTile(pos);

        if (tile != null)
            MoveSelector(tile, stateMachine);
    }

    protected void MoveSelector(TileLogic tile, BaseStateMachine stateMachine)
    {
        Selector.Instance.Tile = tile;
        Selector.Instance.SpriteRenderer.sortingOrder = tile.ContentOrder;
        Selector.Instance.transform.position = tile.WorldPos;
        stateMachine.SelectedTile = tile;
    }
}
