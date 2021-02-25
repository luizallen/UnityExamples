using System.Collections.Generic;
using UnityEngine;

public class CombatMapLoader : MonoBehaviour
{
    public Unit UnitPrefab;

    public static CombatMapLoader Instance;

    public List<Alliance> Alliances;

    public List<Job> Jobs;

    public List<UnitSerialized> SerializedUnits;

    Dictionary<string, Job> _searchJobs;

    GameObject _holder;

    void Awake()
    {
        Instance = this;
        BuildJobsDictonary();
        _holder = new GameObject("Units Holder");
        CombatLog.Append("Começou a partida");
    }

    void Start()
    {
        _holder.transform.parent = Board.Instance.transform;
    }

    public void CreateUnits()
    {
        foreach (var serializedUnit in SerializedUnits)
        {
            CreateUnit(serializedUnit);
        }
    }

    public Unit CreateUnit(UnitSerialized serialized)
    {
        TileLogic tile = Board.GetTile(serialized.Position);

        var unit = Instantiate(UnitPrefab,
                                tile.WorldPos,
                                Quaternion.identity,
                                _holder.transform);

        unit.Tile = tile;
        unit.name = serialized.CharacterName;
        unit.Faction = serialized.Faction;
        unit.PlayerType = serialized.Playertype;
        tile.content = unit.gameObject;

        CombatStateMachineController.Instance.Units.Add(unit);

        var jumper = unit.transform.Find("Jumper");
        jumper.GetComponentInChildren<SpriteRenderer>().sortingOrder = unit.Tile.ContentOrder;

        var jobAsset = _searchJobs[serialized.Job];
        Job.Employ(unit, jobAsset, serialized.Level);

        CreateItem(serialized.Items, unit);

        unit.Experience = Job.GetExpCurveValue(serialized.Level);

        return unit;
    }

    void CreateItem(List<Item> items, Unit unit)
    {
        var equipmentHolder = unit.transform.Find("Equipment");

        foreach (var item in items)
        {
            CreateItem(item, unit, equipmentHolder);
        }
    }

    void CreateItem(Item item, Unit unit, Transform equipmentHolder)
    {
        var instantiated = Instantiate(item, unit.transform.position, Quaternion.identity, equipmentHolder);
        unit.Equipment.Equip(instantiated);
        instantiated.name = instantiated.name.Replace("(Clone)", "");
    }

    void BuildJobsDictonary()
    {
        _searchJobs = new Dictionary<string, Job>();
        foreach (var job in Jobs)
        {
            _searchJobs.Add(job.name, job);
        }
    }   
}
