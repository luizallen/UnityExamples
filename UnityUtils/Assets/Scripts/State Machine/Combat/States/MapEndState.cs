using System.Linq;
using UnityEngine;

public class MapEndState : CombatState
{
    public override void Enter()
    {
        PerformEndState();
        base.Enter();
    }

    void PerformEndState()
    {
        if (!WinCombat())
        {
            StateMachine.EndText.text = "Derrota";
            StateMachine.EndText.color = Color.red;
        } 

        StateMachine.EndPanel.MoveTo("Show");
        CombatLog.Append("O jogo acabou");
    }

    bool WinCombat()
        => StateMachine.Units.Any(u => u.Faction == 0 && u.Active);
}
