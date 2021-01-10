using System.Collections;

public class LoadState : State
{
    public override void Enter()
    {
        StartCoroutine(LoadSequence());
    }

    IEnumerator LoadSequence()
    {
        yield return StartCoroutine(Board.Instance.InitSequence(this));
        yield return null;

        MapLoader.Instance.CreateUnits();
        yield return null;

        InitialTurnOrdering();

        StateMachineController.Instance.ChangeTo<TurnBeginState>();
    }

    void InitialTurnOrdering()
    {
        for (int i = 0; i < StateMachine.Units.Count; i++)
        {
            StateMachine.Units[i].ChargeTime = 100 - StateMachine.Units[i].GetStat(StatEnum.SPEED);
        }
    }
}
