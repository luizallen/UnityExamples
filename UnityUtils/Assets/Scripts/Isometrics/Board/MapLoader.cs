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
        var unit1 = CreateUnit(new Vector3Int(0, -7, 0), "Player", 0, "Mini-Crusader");
        var unit2 = CreateUnit(new Vector3Int(3, -7, 0), "Enemy", 1, "Mini-Crusader");

        StateMachineController.Instance.Units.Add(unit1);
        StateMachineController.Instance.Units.Add(unit2);
    }

    public Unit CreateUnit(Vector3Int pos, string name, int faction, string spriteModel)
    {
        TileLogic tile = Board.GetTile(pos);

        var unit = Instantiate(UnitPrefab,
            tile.WorldPos,
            Quaternion.identity,
            _holder.transform);

        unit.Tile = tile;
        unit.name = name;
        unit.Faction = faction;
        unit.SpriteModel = spriteModel;

        tile.content = unit.gameObject;

        unit.RandomizeStats();

        unit.Stats[StatEnum.HP].BaseValue = unit.GetStat(StatEnum.MAXHP);
        unit.Stats[StatEnum.MP].BaseValue = unit.GetStat(StatEnum.MAXMP);
        unit.UpdateStat();

        return unit;
    }
}
