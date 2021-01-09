using Assets.Scripts.Isometrics.State_Machine.States;

public class MoveSelectionState : State
{
    public override void Enter()
    {
        base.Enter();
        inputs.OnMove += OnMoveTileSelector;
        //inputs.OnFire += OnFire;
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove -= OnMoveTileSelector;
        //inputs.OnFire -= OnFire;
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;

        if (button == 1)
        {
            //stateMachine.ChangeTo<MoveSequenceState>();
        }
        else if (button == 2)
        {
            stateMachine.ChangeTo<ChooseActionState>();
        }
    }
}
