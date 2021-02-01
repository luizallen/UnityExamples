using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public Stats Stats;

    public int Faction;
    public TileLogic Tile;
    public int ChargeTime;
    public bool Active;
    public int Alliance;
    public string SpriteModel;
    public char Direction = 'S';

    public AnimationController AnimationController;

    SpriteSwapper SpriteSwapper;

    public bool Dead { get { return GetStat(StatEnum.HP) <= 0; } }

    void Awake()
    {
        Stats = GetComponentInChildren<Stats>();
        SpriteSwapper = transform.Find("Jumper/Sprite").GetComponent<SpriteSwapper>();

        AnimationController = GetComponent<AnimationController>();
    }

    void Start()
    {
        SpriteSwapper.ThisUnitSprites = SpriteLoader.holder.Find(SpriteModel).GetComponent<SpriteLoader>();
        AnimationController.Idle();
    }

    public int GetStat(StatEnum stat)
    {
        return Stats[stat];
    }

    public void SetStat(StatEnum stat, int value) => Stats[stat] = GetStat(stat) + value;

    public void RandomizeStats()
    {
        foreach (var stats in Stats.StatsList)
        {
            if (stats.Type == StatEnum.MOV)
            {
                stats.Value = Random.Range(1, 5);
                continue;
            }

            stats.Value = Random.Range(1, 300);
        }
    }
}
