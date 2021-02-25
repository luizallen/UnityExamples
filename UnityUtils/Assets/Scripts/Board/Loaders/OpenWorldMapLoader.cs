using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldMapLoader : MonoBehaviour
{
    public Unit UnitPrefab;

    public List<UnitSerialized> SerializedUnits;

    public static OpenWorldMapLoader Instance;

    GameObject _holder;

    void Awake()
    {
        Instance = this;
        _holder = new GameObject("Units Holder");
        Debug.Log($"{PhotonNetwork.NickName} entrou no servidor");
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

        return unit;
    }
}
