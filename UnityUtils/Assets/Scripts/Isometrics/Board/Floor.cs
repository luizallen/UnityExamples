using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
    [HideInInspector]
    public TilemapRenderer TilemapRenderer;
    public int Order { get { return TilemapRenderer.sortingOrder; } }
    public int ContentOrder;
    public Vector3Int MinXY;
    public Vector3Int MaxXY;
    [HideInInspector]
    public Tilemap Tilemap;

    void Awake()
    {
        TilemapRenderer = this.transform.GetComponent<TilemapRenderer>();
        Tilemap = GetComponent<Tilemap>();
    }

    public List<Vector3Int> LoadTiles()
    {
        List<Vector3Int> tiles = new List<Vector3Int>();
        for (int i = MinXY.x; i <= MaxXY.x; i++)
        {
            for (int j = MinXY.y; j <= MaxXY.y; j++)
            {
                Vector3Int currentPos = new Vector3Int(i, j, 0);
                if (Tilemap.HasTile(currentPos))
                {
                    tiles.Add(currentPos);
                }
            }
        }
        return tiles;
    }

}