using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Dictionary<Vector3Int, TileLogic> Tiles;
    public List<Floor> Floors;
    public static Board Instance;
    [HideInInspector]
    public Grid Grid;

    void Awake()
    {
        Tiles = new Dictionary<Vector3Int, TileLogic>();
        Instance = this;
        Grid = GetComponent<Grid>();
    }

    public IEnumerator InitSequence(LoadState loadState)
    {
        yield return StartCoroutine(LoadFloors(loadState));
        yield return null;
        Debug.Log("Foram criados " + Tiles.Count + " tiles");
        ShadowOrdering();
        yield return null;
    }

    IEnumerator LoadFloors(LoadState loadState)
    {
        for (int i = 0; i < Floors.Count; i++)
        {
            List<Vector3Int> floorTiles = Floors[i].LoadTiles();
            yield return null;
            for (int j = 0; j < floorTiles.Count; j++)
            {   
                if (!Tiles.ContainsKey(floorTiles[j]))
                {
                    CreateTile(floorTiles[j], Floors[i]);
                }
            }
        }
    }

    void CreateTile(Vector3Int pos, Floor floor)
    {
        Vector3 worldPos = Grid.CellToWorld(pos);
        worldPos.y += (floor.Tilemap.tileAnchor.y / 2);
        TileLogic tileLogic = new TileLogic(pos, worldPos, floor);
        Tiles.Add(pos, tileLogic);
    }

    void ShadowOrdering()
    {
        foreach (TileLogic t in Tiles.Values)
        {
            int floorIndex = Floors.IndexOf(t.Floor);
            floorIndex -= 2;

            if (floorIndex >= Floors.Count || floorIndex < 0)
            {
                continue;
            }
            Floor floorToCheck = Floors[floorIndex];

            Vector3Int pos = t.Pos;
            IsNECheck(floorToCheck, t, pos + Vector3Int.right);
            IsNECheck(floorToCheck, t, pos + Vector3Int.up);
            IsNECheck(floorToCheck, t, pos + Vector3Int.right + Vector3Int.up);
        }
    }

    void IsNECheck(Floor floor, TileLogic t, Vector3Int NEPosition)
    {
        if (floor.Tilemap.HasTile(NEPosition))
        {
            t.ContentOrder = floor.Order;
        }
    }

    public static TileLogic GetTile(Vector3Int pos)
    {
        Instance.Tiles.TryGetValue(pos, out var tile);

        return tile;
    }
}
