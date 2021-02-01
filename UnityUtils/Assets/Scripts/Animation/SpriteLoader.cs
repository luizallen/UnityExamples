using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SpriteLoader : MonoBehaviour
{
    public static readonly List<string> AnimationNames = new List<string> { "Idle", "Walk", "Attack", "GotHit", "Jump", "Death" };
    public static readonly List<char> Directions = new List<char> { 'N', 'S', 'E', 'W' };

    public List<Animation2D> Animations;
    public Dictionary<string, Animation2D> AnimationsFinder;

    public float FrameRate;
    public float DelayStart;

    public static Transform holder;

    void Awake()
    {
        if (holder == null)
            holder = transform.parent;
    }

    public IEnumerator Load()
    {
        Animations = new List<Animation2D>();
        AnimationsFinder = new Dictionary<string, Animation2D>();

        foreach (var animation in AnimationNames)
        {
            foreach (var direction in Directions)
            {
                var tempName = new string[1] { transform.name + "/" + animation + direction };

                var tempAnimation2D = new Animation2D();
                tempAnimation2D.FrameRate = FrameRate;
                tempAnimation2D.DelayStart = DelayStart;
                tempAnimation2D.Name = animation + direction;
                tempAnimation2D.Frames = new List<Sprite>();
                Animations.Add(tempAnimation2D);
                AnimationsFinder.Add(tempAnimation2D.Name, tempAnimation2D);

                yield return Addressables.LoadAssetsAsync<Sprite>(tempName, Callback, Addressables.MergeMode.Union);

                tempAnimation2D.Frames = tempAnimation2D.Frames.OrderBy(f => f.name).ToList();
            }
        }
    }

    void Callback(Sprite op)
    {
        if (op != null)
            Animations[Animations.Count - 1].Frames.Add(op);
    }

    public Animation2D GetAnimation(string name)
    {
        AnimationsFinder.TryGetValue(name, out var rtv);
        return rtv;
    }
}
