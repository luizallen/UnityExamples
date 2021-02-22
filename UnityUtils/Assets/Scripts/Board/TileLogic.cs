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
    public int Height { get { return Floor.Height; } }
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

    public char GetDirection(TileLogic tileLogic) => GetDirection(tileLogic.Pos);

    public char GetDirection(Vector3Int pos)
    {
        if (Pos.y < pos.y)
            return 'N';

        if (Pos.x < pos.x)
            return 'E';

        if (Pos.y > pos.y)
            return 'S';

        return 'W';
    }
}
