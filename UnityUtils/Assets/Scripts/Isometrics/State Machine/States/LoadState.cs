using System.Collections;
using UnityEngine;

public class LoadState : State
{
    public override void Enter()
    {
        StartCoroutine(LoadSequence());

    }

    IEnumerator LoadSequence()
    {
        yield return StartCoroutine(Board.instance.InitSequence(this));
        //
        //
        //
        yield return null;
        StateMachineController.instance.ChangeTo<RoamState>();
    }
}
