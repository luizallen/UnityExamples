using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public Unit unitPrefab;

    public static MapLoader instance;

    GameObject holder;

    void Awake()
    {
        instance = this;
        holder = new GameObject("Units Holder");
    }

    void Start()
    {
        holder.transform.parent = Board.instance.transform;
    }

    public void CreateUnits()
    {
        var unit1 = CreateUnit(new Vector3Int(-1, -5, 0), "Player", 0);
        var unit2 = CreateUnit(new Vector3Int(3, -5, 0), "Enemy", 1);

        StateMachineController.instance.units.Add(unit1);
        StateMachineController.instance.units.Add(unit2);
    }

    public Unit CreateUnit(Vector3Int pos, string name, int faction)
    {
        TileLogic tile = Board.GetTile(pos);

        var unit = Instantiate(unitPrefab,
            tile.worldPos,
            Quaternion.identity,
            holder.transform);

        unit.tile = tile;
        unit.name = name;
        unit.faction = faction;

        return unit;
    }
}
