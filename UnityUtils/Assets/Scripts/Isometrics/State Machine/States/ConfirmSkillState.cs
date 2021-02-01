using UnityEngine;

public class ConfirmSkillState : State
{
    public override void Enter()
    {
        base.Enter();
        Inputs.OnFire += OnFire;

        Turn.Targets = Turn.Skill.GetArea();
        Board.SelectTiles(Turn.Targets, Turn.Unit.Alliance);
    }

    public override void Exit()
    {
        base.Exit();
        Inputs.OnFire -= OnFire;

        Board.DeSelectTiles(Turn.Targets);
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;

        if (button == 1)
        {
            if (Turn.Skill.ValidadeTarget(Turn.Targets))
            {
                StateMachine.ChangeTo<PerformSkillState>();
            }
            else
                Debug.Log("No unit would be affected");
        }
        else if (button == 2)
        {
            StateMachine.ChangeTo<SkillTargetState>();
        }
    }
}
