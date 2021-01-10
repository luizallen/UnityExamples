using Assets.Scripts.Isometrics.State_Machine.States;

public class MoveSelectionState : State
{
    public override void Enter()
    {
        base.Enter();
        Inputs.OnMove += OnMoveTileSelector;
        Inputs.OnFire += OnFire;
    }

    public override void Exit()
    {
        base.Exit();
        Inputs.OnMove -= OnMoveTileSelector;
        Inputs.OnFire -= OnFire;
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;

        if (button == 1)
        {
            StateMachine.ChangeTo<MoveSequenceState>();
        }
        else if (button == 2)
        {
            StateMachine.ChangeTo<ChooseActionState>();
        }
    }
}
