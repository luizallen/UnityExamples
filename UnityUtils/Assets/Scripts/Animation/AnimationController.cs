using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Unit Unit;
    SpriteSwapper SpriteSwapper;

    void Awake()
    {
        Unit = GetComponent<Unit>();
        SpriteSwapper = transform.Find("Jumper/Sprite").GetComponent<SpriteSwapper>();
    }

    public void Idle() => Play("Idle");

    public void Walk() => Play("Walk");

    public void Attack() => SpriteSwapper.PlayThenReturn("Attack" + Unit.Direction);

    public void GotHit() => SpriteSwapper.PlayThenReturn("GotHit" + Unit.Direction);

    public void GotHit(float delay) => Invoke("GotHit", delay);

    public void Death() => SpriteSwapper.PlayThenStop("Death" + Unit.Direction);

    public void Death(float delay) => Invoke("Death", delay);

    public float Jump()
    {
        Play("Jump");
        return GetAnimationTimer("Jump" + Unit.Direction);
    }

    public float GetAnimationTimer(string animeName)
    {
        var animation = SpriteSwapper.ThisUnitSprites.GetAnimation(animeName);
        var timePerFrame = 1 / animation.FrameRate;

        return animation.Frames.Count * timePerFrame;
    }

    void Play(string animName)
    {
        animName += Unit.Direction;

        if (SpriteSwapper.Current.Name != animName)
            SpriteSwapper.PlayAnimation(animName);
    }    
}
