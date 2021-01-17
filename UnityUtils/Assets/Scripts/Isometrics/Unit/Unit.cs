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

    void Awake() => Stats = GetComponentInChildren<Stats>();

    public int GetStat(StatEnum stat)
    {
        return Stats.StatsList[(int)stat].Value;
    }

    public void RandomizeStats()
    {
        foreach (var stats in Stats.StatsList)
        {
            if(stats.Type == StatEnum.MOV)
            {
                stats.Value = Random.Range(1, 5);
                continue;
            }

            stats.Value = Random.Range(1, 300);
        }
    }

    public void SetStat(StatEnum stat, int value) => Stats.StatsList[(int)stat].Value = GetStat(stat) + value;
}
