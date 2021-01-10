using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public Unit UnitPrefab;

    public static MapLoader Instance;

    GameObject _holder;

    void Awake()
    {
        Instance = this;
        _holder = new GameObject("Units Holder");
    }

    void Start()
    {
        _holder.transform.parent = Board.Instance.transform;
    }

    public void CreateUnits()
    {
        var unit1 = CreateUnit(new Vector3Int(1, -5, 0), "Player", 0);
        var unit2 = CreateUnit(new Vector3Int(1, -4, 0), "Enemy", 1);

        StateMachineController.Instance.Units.Add(unit1);
        StateMachineController.Instance.Units.Add(unit2);
    }

    public Unit CreateUnit(Vector3Int pos, string name, int faction)
    {
        TileLogic tile = Board.GetTile(pos);

        Debug.Log("Chegou Aqui");

        var unit = Instantiate(UnitPrefab,
            tile.WorldPos,
            Quaternion.identity,
            _holder.transform);

        unit.Tile = tile;
        unit.name = name;
        unit.Faction = faction;

        return unit;
    }
}
