using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public Unit UnitPrefab;

    public static MapLoader Instance;

    public List<Alliance> Alliances;

    GameObject _holder;

    void Awake()
    {
        Instance = this;
        _holder = new GameObject("Units Holder");
        CombatLog.Append("Começou a partida");
    }

    void Start()
    {
        _holder.transform.parent = Board.Instance.transform;
    }

    public void CreateUnits()
    {
        var unit1 = CreateUnit(new Vector3Int(0, -7, 0), "Player", 0);
        var unit2 = CreateUnit(new Vector3Int(3, -7, 0), "Enemy", 1);

        StateMachineController.Instance.Units.Add(unit1);
        StateMachineController.Instance.Units.Add(unit2);
    }

    public Unit CreateUnit(Vector3Int pos, string name, int faction)
    {
        TileLogic tile = Board.GetTile(pos);

        var unit = Instantiate(UnitPrefab,
            tile.WorldPos,
            Quaternion.identity,
            _holder.transform);

        unit.Tile = tile;
        unit.name = name;
        unit.Faction = faction;

        tile.content = unit.gameObject;

        unit.RandomizeStats();

        return unit;
    }

    void InitializeAlliances()
    {
        for (int i = 0; i < Alliances.Count; i++)
        {
            Alliances[i].Units = new List<Unit>();
        }
    }
}
