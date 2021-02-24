using Assets.Scripts.Isometrics.State_Machine.States;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectionState : CombatUIState
{
    List<Consumable> _consumables;

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
        CheckItems();
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
            Turn.IsItem = null;
            ChangeUISelector(StateMachine.SkillSelectionButtons);
        }
    }

    void CheckItems()
    {
        _consumables = new List<Consumable>();

        var bag = (int)ItemSlot.Bag1;
        for (int i = 0; i < 2; i++, bag++)
        {
            var item = Turn.Unit.Equipment.GetItem((ItemSlot)bag);
            if (item != null)
                _consumables.Add(item.GetComponent<Consumable>());
        }

        for (int i = 0; i < 5; i++)
        {
            if (i < _consumables.Count)
            {
                if (_consumables[i].Icon == null)
                    StateMachine.SkillSelectionButtons[i].sprite = _consumables[i].Skill.Icon;
                else
                    StateMachine.SkillSelectionButtons[i].sprite = _consumables[i].Icon;
            }
            else
            {
                StateMachine.SkillSelectionButtons[i].sprite = StateMachine.SkillSelectionBlocked;
            }
        }
    }

    void ActionButtons()
    {
        if (Index >= _consumables.Count)
            return;

        var actualItem = _consumables[Index];

        if (actualItem.Skill.CanUse())
        {
            CombatLog.Append(string.Format("O {0} está usando a habilidade {1}", Turn.Unit.name, actualItem.name));
            Turn.Skill = actualItem.Skill;
            Turn.IsItem = actualItem;
            StateMachine.ChangeTo<SkillTargetState>();
        }
    }
}
