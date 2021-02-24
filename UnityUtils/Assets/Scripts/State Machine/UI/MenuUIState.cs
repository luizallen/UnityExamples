public class MenuUIState : State
{
    protected UIStateMachineController StateMachine { get { return UIStateMachineController.Instance; } }
    protected NetworkController Network { get { return NetworkController.Instance; } }
}
