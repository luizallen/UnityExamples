using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Unit Unit;
    SpriteSwapper _spriteSwapper;

    void Awake()
    {
        Unit = GetComponent<Unit>();
        _spriteSwapper = transform.Find("Jumper/Sprite").GetComponent<SpriteSwapper>();
    }

    public void Idle() => Play("Idle");

    public void Walk() => Play("Walk");

    public void Attack() => _spriteSwapper.PlayThenReturn("Attack" + Unit.Direction);

    public void GotHit() => _spriteSwapper.PlayThenReturn("GotHit" + Unit.Direction);

    public void GotHit(float delay) => Invoke("GotHit", delay);

    public void Death() => _spriteSwapper.PlayThenStop("Death" + Unit.Direction);

    public void Death(float delay) => Invoke("Death", delay);

    public float Jump()
    {
        Play("Jump");
        return GetAnimationTimer("Jump" + Unit.Direction);
    }

    public float GetAnimationTimer(string animeName)
    {
        var animation = _spriteSwapper.ThisUnitSprites.GetAnimation(animeName);
        var timePerFrame = 1 / animation.FrameRate;

        return animation.Frames.Count * timePerFrame;
    }

    void Play(string animName)
    {
        animName += Unit.Direction;

        if (_spriteSwapper.Current.Name != animName)
            _spriteSwapper.PlayAnimation(animName);
    }
}
