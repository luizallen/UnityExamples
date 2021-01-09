using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public Stats stats;

    public int faction;
    public TileLogic tile;

    void Awake()
    {
        stats = GetComponentInChildren<Stats>();
    }
}
