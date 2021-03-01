using Assets.Scripts.Isometrics.State_Machine.States;
using UnityEngine;

public class JobAdvanceState : CombatState
{
    public override void Enter()
    {
        StateMachine.JobAdvancePanel.Show();
        Inputs.OnMove += OnMove;
        Inputs.OnFire += OnFire;
    }

    public override void Exit()
    {
        StateMachine.JobAdvancePanel.Hide();
        Inputs.OnMove -= OnMove;
        Inputs.OnFire -= OnFire;
    }

    void OnMove(object sender, object args)
    {
        var button = (Vector3Int)args;

        if (button == Vector3Int.left)
            StateMachine.JobAdvancePanel.SelectPrevious();
        else if (button == Vector3Int.right)
            StateMachine.JobAdvancePanel.SelectNext();
    }

    void OnFire(object sender, object args)
    {
        var mouse = (Mouse)args;

        if (mouse.Button == 1)
        {
            StateMachine.JobAdvancePanel.JobChange();
            StateMachine.ChangeTo<ChooseActionState>();
        }
    }
}
