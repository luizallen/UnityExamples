using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        yield return LoadAnimations();
        yield return null;

        MapLoader.Instance.CreateUnits();
        yield return null;

        InitialTurnOrdering();
        yield return null;

        UnitAlliances();
        yield return null;

        var blockers = Blockers.Instance.GetBlockers();
        yield return null;

        SetBlockers(blockers);
        yield return null;

        StateMachineController.Instance.ChangeTo<TurnBeginState>();
    }

    IEnumerator LoadAnimations()
    {
        var loaders = SpriteLoader.holder.GetComponentsInChildren<SpriteLoader>();
        foreach (var loader in loaders)
        {
            yield return loader.Load();
        }
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
                unit.Alliance = unit.Faction;
                return;
            }
        }
    }

    void SetBlockers(List<Vector3Int> blockers)
    {
        foreach (var pos in blockers)
        {
            var tileMap = Board.GetTile(pos);
            tileMap.content = Blockers.Instance.gameObject;
        }
    }
}
