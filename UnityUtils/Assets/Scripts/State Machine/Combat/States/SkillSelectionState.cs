using Assets.Scripts.Isometrics.State_Machine.States;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectionState : CombatUIState
{
    List<Skill> _skillList;

    public override void Enter()
    {
        base.Enter();
        MoveSelector(Turn.Unit.Tile);
        Index = 0;

        Inputs.OnMove += OnMove;
        Inputs.OnFire += OnFire;

        CurrentUISelector = StateMachine.SkillSelectionSelection;
        StateMachine.SkillSelectionPanel.MoveTo("Show");
        StateMachine.LeftCharacterPanel.Show(Turn.Unit);

        ChangeUISelector(StateMachine.SkillSelectionButtons);
        CheckSkills();
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
            ActionButtons();
        }
        else if (button == 2)
        {
            StateMachine.LeftCharacterPanel.Hide();
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

    void CheckSkills()
    {
        //Não é recomendado para jogos dinamicos.
        var skillBook = Turn.Unit.transform.Find("SkillBook");

        _skillList = new List<Skill>();
        _skillList.AddRange(skillBook.GetComponent<SkillBook>().Skills);

        for (int i = 0; i < 5; i++)
        {
            if (i < _skillList.Count)
            {
                StateMachine.SkillSelectionButtons[i].sprite = _skillList[i].Icon;
            }
            else
            {
                StateMachine.SkillSelectionButtons[i].sprite = StateMachine.SkillSelectionBlocked;
            }
        }
    }

    void ActionButtons()
    {
        if (Index >= _skillList.Count)
            return;

        var actualSkill = _skillList[Index];

        if (actualSkill.CanUse())
        {
            CombatLog.Append(string.Format("O {0} está usando a habilidade {1}", Turn.Unit.name, actualSkill.name));
            Turn.Skill = actualSkill;
            StateMachine.ChangeTo<SkillTargetState>();
        }
    }
}
