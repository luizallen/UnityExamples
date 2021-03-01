using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Dictionary<Vector3Int, TileLogic> Tiles;
    public List<Floor> Floors;
    public static Board Instance;
    [HideInInspector]
    public Grid Grid;
    public List<Tile> Highlights;

    //Directions
    public Vector3Int[] Directions = new Vector3Int[4]
    {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right,
    };

    void Awake()
    {
        Tiles = new Dictionary<Vector3Int, TileLogic>();
        Instance = this;
        Grid = GetComponent<Grid>();
    }

    public IEnumerator InitSequence(State loadState)
    {
        yield return StartCoroutine(LoadFloors(loadState));
        yield return null;
        ShadowOrdering();
        yield return null;
    }

    IEnumerator LoadFloors(State loadState)
    {
        foreach (var floor in Floors)
        {
            List<Vector3Int> floorTiles = floor.LoadTiles();
            yield return null;

            foreach (var floorTile in floorTiles)
            {
                if (!Tiles.ContainsKey(floorTile))
                {
                    CreateTile(floorTile, floor);
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
        => Instance.Tiles.TryGetValue(pos, out var tile) ? tile : null;

    public void SelectTiles(List<TileLogic> tiles, int allianceIndex)
    {
        foreach (var tile in tiles)
        {
            tile.Floor.Highlight.SetTile(tile.Pos, Highlights[allianceIndex]);
        }
    }

    public void DeSelectTiles(List<TileLogic> tiles)
    {
        foreach (var tile in tiles)
        {
            tile.Floor.Highlight.SetTile(tile.Pos, null);
        }
    }

    public List<TileLogic> Search(TileLogic start, Func<TileLogic, TileLogic, bool> searchType)
    {
        var tilesSearch = new List<TileLogic>();

        tilesSearch.Add(start);
        ClearSearch();

        var checkNext = new Queue<TileLogic>();
        var checkNow = new Queue<TileLogic>();

        start.Distance = 0;
        checkNow.Enqueue(start);

        while (checkNow.Count > 0)
        {
            var tile = checkNow.Dequeue();

            foreach (var direction in Directions)
            {
                var next = GetTile(tile.Pos + direction);

                if (next == null || next.Distance <= tile.Distance + 1)
                    continue;

                if (searchType(tile, next))
                {
                    next.Prev = tile;
                    checkNext.Enqueue(next);
                    tilesSearch.Add(next);
                }
            }

            if (checkNow.Count == 0)
                SwapReference(ref checkNow, ref checkNext);
        }

        return tilesSearch;
    }

    private void SwapReference(ref Queue<TileLogic> checkNow, ref Queue<TileLogic> checkNext)
    {
        var temp = checkNow;
        checkNow = checkNext;
        checkNext = temp;
    }

    void ClearSearch()
    {
        foreach (var tile in Tiles.Values)
        {
            tile.Prev = null;
            tile.Distance = int.MaxValue;
        }
    }
}
