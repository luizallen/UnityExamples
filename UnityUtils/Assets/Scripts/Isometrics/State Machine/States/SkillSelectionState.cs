using Assets.Scripts.Isometrics.State_Machine.States;
using UnityEngine;

public class SkillSelectionState : UIState
{
    public override void Enter()
    {
        base.Enter();
        Index = 0;

        Inputs.OnMove += OnMove;
        Inputs.OnFire += OnFire;

        CurrentUISelector = StateMachine.SkillSelectionSelection;
        StateMachine.SkillSelectionPanel.MoveTo("Show");

        ChangeUISelector(StateMachine.SkillSelectionButtons);
        //CheckActions();
    }

    public override void Exit()
    {
        base.Exit();

        Inputs.OnMove -= OnMove;
        Inputs.OnFire -= OnFire;

        StateMachine.SkillSelectionPanel.MoveTo("Hide");
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;

        if (button == 1)
        {
            //ActionButtons();
        }
        else if (button == 2)
        {
            StateMachine.ChangeTo<ChooseActionState>();
        }
    }

    void OnMove(object sender, object args)
    {
        var button = (Vector3Int)args;

        if (button == Vector3Int.up)
        {
            Index--;
            ChangeUISelector(StateMachine.SkillSelectionButtons);
        }
        else if (button == Vector3Int.down)
        {
            Index++;
            ChangeUISelector(StateMachine.SkillSelectionButtons);
        }
    }

}
