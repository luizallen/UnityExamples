using Assets.Scripts.Network;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldMapLoader : MonoBehaviour
{
    public Unit UnitPrefab;
    public static OpenWorldMapLoader Instance;

    public List<Unit> InstantiatedUnits;

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

    public Unit CreateUnit(string playerName)
    {
        var initialPosition = new Vector3Int(-5, -5, 0);

        TileLogic tile = Board.GetTile(initialPosition);

        //var instantiatedUnit = PhotonNetwork.Instantiate(UnitPrefab.name,
        //                        tile.WorldPos,
        //                        Quaternion.identity,
        //                        0);

        //var unit = instantiatedUnit.GetComponent<Unit>();

        var unit = Instantiate(UnitPrefab,
                                tile.WorldPos,
                                Quaternion.identity,
                                _holder.transform);

        unit.Tile = tile;
        unit.name = playerName;
        unit.PlayerType = PlayerType.Human;
        unit.PlayerName.text = NetworkConfig.PlayerName;

        OpenWorldStateMachine.Instance.Player = unit;

        var jumper = unit.transform.Find("Jumper");
        jumper.GetComponentInChildren<SpriteRenderer>().sortingOrder = unit.Tile.ContentOrder;

        return unit;
    }
}
