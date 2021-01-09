using UnityEngine;

public class State : MonoBehaviour
{
    protected InputController inputs { get { return InputController.instance; } }

    protected StateMachineController stateMachine { get { return StateMachineController.instance; } }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    protected void OnMoveTileSelector(object sender, object args)
    {
        Vector3Int input = (Vector3Int)args;
        TileLogic tile = Board.GetTile(Selector.instance.position + input);

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
        Selector.instance.tile = tile;
        Selector.instance.spriteRenderer.sortingOrder = tile.contentOrder;
        Selector.instance.transform.position = tile.worldPos;
        stateMachine.selectedTile = tile;
    }
}
