using System.Collections.Generic;
using UnityEngine;

public class LineSkillRange : SkillRange
{
    void Awake()
    {
        DirectionOriented = true;
    }

    public override List<TileLogic> GetTileInRange()
    {
        var unit = Turn.Unit;
        var startPos = unit.Tile.Pos;
        var retValue = new List<TileLogic>();

        Vector3Int direction;

        switch (unit.Direction)
        {
            case 'N':
                direction = new Vector3Int(0, 1, 0);
                break;
            case 'S':
                direction = new Vector3Int(0, -1, 0);
                break;
            case 'E':
                direction = new Vector3Int(1, 0, 0);
                break;
            default:
                direction = new Vector3Int(-1, 0, 0);
                break;
        }

        var currentPos = startPos;

        for (int i = 1; i <= Range; i++)
        {
            currentPos += direction;
            var tile = Board.GetTile(currentPos);

            if (tile != null && Mathf.Abs(tile.Floor.Height - unit.Tile.Floor.Height) <= VerticalRange)
                retValue.Add(tile);
        }

        return retValue;
    }
}
