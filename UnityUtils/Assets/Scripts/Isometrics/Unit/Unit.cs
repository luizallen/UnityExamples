using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public Stats Stats;

    public int Faction;
    public TileLogic Tile;
    public int ChargeTime;

    void Awake()
    {
        Stats = GetComponentInChildren<Stats>();
    }

    public int GetStat(StatEnum stat)
    {
        return Stats.StatsList[(int)stat].Value;
    }
}
