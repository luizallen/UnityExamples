public class OpenWorldState : State
{
    protected OpenWorldStateMachine StateMachine { get { return OpenWorldStateMachine.Instance; } }

    protected Board Board { get { return Board.Instance; } }
}
