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
        yield return null;

        UnitAlliances();

        StateMachineController.Instance.ChangeTo<TurnBeginState>();
    }

    void InitialTurnOrdering()
    {
        foreach (var unit in StateMachine.Units)
        {
            unit.ChargeTime = 101 - unit.GetStat(StatEnum.SPEED);
            unit.Active = true;
        }
    }

    void UnitAlliances()
    {
        foreach (var unit in StateMachine.Units)
        {
            SetUnitAlliance(unit);
        }
    }

    void SetUnitAlliance(Unit unit)
    {
        foreach (var alliances in MapLoader.Instance.Alliances)
        {
            if (alliances.Factions.Contains(unit.Faction))
            {
                alliances.Units.Add(unit);
                return;
            }
        }
    }
}
