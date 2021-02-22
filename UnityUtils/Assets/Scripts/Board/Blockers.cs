using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Blockers : MonoBehaviour
{
    public static Blockers Instance;

    void Awake()
    {
        Instance = this;
        GetComponent<TilemapRenderer>().enabled = false;
    }

    public List<Vector3Int> GetBlockers()
    {
        var tileMap = GetComponent<Tilemap>();

        var blockeds = new List<Vector3Int>();

        var bounds = tileMap.cellBounds;

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                blockeds.Add(new Vector3Int(pos.x, pos.y, 0));
            }
        }

        return blockeds;
    }
}
