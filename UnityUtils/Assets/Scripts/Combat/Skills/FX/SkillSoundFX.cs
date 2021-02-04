using UnityEngine;

public class SkillSoundFX : MonoBehaviour
{
    public AudioClip Sound;
    public float Delay;

    public void Play()
    {
        Turn.Unit.AudioSource.clip = Sound;
        Turn.Unit.AudioSource.PlayDelayed(Delay);
    }
}
