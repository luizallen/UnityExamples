using UnityEngine;

public class TileLogic
{
    public Vector3Int Pos;
    public Vector3 WorldPos;
    public GameObject content;
    public Floor Floor;
    public int ContentOrder;

    #region pathFinding
    public TileLogic Prev;
    public float Distance;
    #endregion

    //public TileType tileType;

    public TileLogic() { }

    public TileLogic(Vector3Int cellPos, Vector3 worldPosition, Floor tempFloor)
    {
        Pos = cellPos;
        WorldPos = worldPosition;
        Floor = tempFloor;
        ContentOrder = tempFloor.ContentOrder;
    }

    public static TileLogic Create(Vector3Int cellPos, Vector3 worldPosition, Floor tempFloor)
    {
        TileLogic tileLogic = new TileLogic(cellPos, worldPosition, tempFloor);
        return tileLogic;
    }
}
