public class OpenWorldState : State
{
    protected OpenWorldStateMachine StateMachine => OpenWorldStateMachine.Instance;

    protected Board Board => Board.Instance;

    protected Unit Unit => StateMachine.Player;
}
