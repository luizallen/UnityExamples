using System;
using System.Collections;

public class LoadState : State
{
    public override void Enter()
    {
        StartCoroutine(LoadSequence());
    }

    IEnumerator LoadSequence()
    {
        yield return StartCoroutine(Board.instance.InitSequence(this));
        yield return null;

        MapLoader.instance.CreateUnits();
        yield return null;

        InitialTurnOrdering();

        StateMachineController.instance.ChangeTo<RoamState>();
    }

    void InitialTurnOrdering()
    {
        int first = new Random().Next(0, stateMachine.units.Count);

        Turn.hasActed = false;
        Turn.hasMoved = false;
        Turn.unit = stateMachine.units[first];
    }
}
