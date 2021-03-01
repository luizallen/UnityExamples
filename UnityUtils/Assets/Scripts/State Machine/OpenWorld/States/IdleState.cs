public class IdleState : OpenWorldState
{
    public override void Enter()
    {
        Inputs.OnFire += OnFire;
        Inputs.OnMoveMouse += OnMoveMouse;
        base.Enter();
    } 

    public override void Exit()
    {
        Inputs.OnFire -= OnFire;
        Inputs.OnMoveMouse -= OnMoveMouse;
        base.Exit();
    }

    void OnFire(object sender, object args)
    {
        var mouse = (Mouse)args;

        if (mouse.Button == 1)
        {
            var teste = mouse.Position;

            StopAllCoroutines();

            var tiles =  Board.Instance.Search(Unit.Tile, Unit.GetComponent<Movement>().ValidateGlobalMovement);
            tiles.Remove(Unit.Tile);

            var tile = Board.GetTile(mouse.Position);

            if (tile == null || !tiles.Contains(tile))
                return;

            var unit = StateMachine.Player;
            var movement = unit.GetComponent<Movement>();

            StartCoroutine(movement.Move(unit.Tile, tile, unit));
        }
        else if (mouse.Button == 2)
        {
            //OpenMenuDialog
        }
    }

    void OnMoveMouse(object sender, object args)
    {
        var mouse = (Mouse)args;
        MoveSelector(mouse.Position, StateMachine);
    }
}
