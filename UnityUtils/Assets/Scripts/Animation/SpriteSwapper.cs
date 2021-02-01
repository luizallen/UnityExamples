using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;

    public SpriteLoader ThisUnitSprites;
    public Animation2D Current;
    public Coroutine Playing;
    public Queue<Animation2D> Sequence;

    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Sequence = new Queue<Animation2D>();
    }
    
    public void Stop()
    {
        if (Playing != null)
            StopCoroutine(Playing);

        Sequence.Clear();
    }

    public void PlayAnimation(string name)
    {
        Stop();
        var teste = ThisUnitSprites.GetAnimation(name);

        Sequence.Enqueue(teste);
        Playing = StartCoroutine(Play());
    }

    public void PlayAnimations(List<string> names)
    {
        Stop();

        foreach (var name in names)
        {
            Sequence.Enqueue(ThisUnitSprites.GetAnimation(name));
        }

        Playing = StartCoroutine(Play());
    }

    public void PlayThenReturn(string name)
    {
        var toPlay = ThisUnitSprites.GetAnimation(name);

        Stop();

        Sequence.Enqueue(toPlay);
        Sequence.Enqueue(Current);

        Playing = StartCoroutine(Play());
    }

    public void PlayAtTheEnd(string name)
    {
        var toPlay = ThisUnitSprites.GetAnimation(name);
        Sequence.Enqueue(toPlay);
    }

    public void PlayThenStop(string name)
    {
        Stop();
        Sequence.Enqueue(ThisUnitSprites.GetAnimation(name));
        Playing = StartCoroutine(PlayOnce());
    }

    IEnumerator Play()
    {
        while (true)
        {
            Current = Sequence.Dequeue();

            if (Sequence.Count == 0)
                Sequence.Enqueue(Current);

            var timePerFrame = 1 / Current.FrameRate;

            foreach (var frame in Current.Frames)
            {
                SpriteRenderer.sprite = frame;
                yield return new WaitForSeconds(timePerFrame);
            }

            yield return null;
        }
    }

    IEnumerator PlayOnce()
    {
        Current = Sequence.Dequeue();

        var timePerFrame = 1 / Current.FrameRate;

        foreach (var frame in Current.Frames)
        {
            SpriteRenderer.sprite = frame;
            yield return new WaitForSeconds(timePerFrame);
        }
    }
}
