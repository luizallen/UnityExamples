using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public Unit UnitPrefab;

    public static MapLoader Instance;

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

        tile.content = unit.gameObject;

        StateMachineController.Instance.Units.Add(unit);

        var jobAsset = _searchJobs[serialized.Job];
        Job.Employ(unit, jobAsset, serialized.Level);

        unit.Experience = Job.GetExpCurveValue(serialized.Level);

        return unit;
    }

    private void BuildJobsDictonary()
    {
        _searchJobs = new Dictionary<string, Job>();
        foreach (var job in Jobs)
        {
            _searchJobs.Add(job.name, job);
        }
    }   
}
