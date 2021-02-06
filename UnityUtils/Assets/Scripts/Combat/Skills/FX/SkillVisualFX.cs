using UnityEngine;

public class SkillVisualFX : MonoBehaviour
{
    public Vector3 offset;

    public ParticleSystem Always;

    public ParticleSystem OnlyOnHit;

    public float Delay;

    [HideInInspector]
    public bool DidHit;

    [HideInInspector]
    public Unit Target;


    public void VFX(Unit target)
    {
        Invoke("VFXDelay", Delay);
    }

    void VFXDelay()
    {
        if (Always != null)
        {
            Always.gameObject.layer = 1;
            SpawnEffect(Always);
        }

        if (DidHit && OnlyOnHit != null)
            SpawnEffect(OnlyOnHit);
    }

    void SpawnEffect(ParticleSystem toSpawn) 
        => Instantiate(toSpawn, Target.SpriteSwapper.transform.position + offset, Quaternion.identity, Target.SpriteSwapper.transform);
}
