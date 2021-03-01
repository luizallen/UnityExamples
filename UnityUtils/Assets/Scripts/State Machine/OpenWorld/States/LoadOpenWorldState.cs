using Assets.Scripts.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOpenWorldState : OpenWorldState
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

        var unit = OpenWorldMapLoader.Instance.CreateUnit(NetworkConfig.PlayerName);
        StateMachine.Player = unit;
        yield return null;

        var blockers = Blockers.Instance.GetBlockers();
        yield return null;

        SetBlockers(blockers);
        yield return null;

        Time.timeScale = 3;
        OpenWorldStateMachine.Instance.ChangeTo<IdleState>();
    }

    IEnumerator LoadAnimations()
    {
        var loaders = SpriteLoader.holder.GetComponentsInChildren<SpriteLoader>();
        foreach (var loader in loaders)
        {
            yield return loader.Load();
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

