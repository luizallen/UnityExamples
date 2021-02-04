using System.Collections.Generic;
using UnityEngine;

public class ConeSkillRange : SkillRange
{
    public override bool IsDirectionOriented() => true;

    public override List<TileLogic> GetTileInRange()
    {
        var unit = Turn.Unit;
        var pos = unit.Tile.Pos;
        var retValue = new List<TileLogic>();
        var lateral = 1;

        for (var i = 1; i <= Range; i++)
        {
            var min = -(lateral / 2);
            var max = (lateral / 2);

            for (var j = min; j <= max; j++)
            {
                var next = GetNext(unit.Direction, pos, i, j);
                var tile = Board.GetTile(next);

                if (ValidTile(tile))
                    retValue.Add(tile);
            }

            lateral += 2;
        }


        return retValue;
    }

    bool ValidTile(TileLogic tileLogic)
        => tileLogic != null && Mathf.Abs(tileLogic.Floor.Height - Turn.Unit.Tile.Floor.Height) <= VerticalRange;

    Vector3Int GetNext(char orientation, Vector3Int pos, int arg1, int arg2)
    {
        var next = Vector3Int.zero;

        switch (orientation)
        {
            case 'N':
                next = new Vector3Int(pos.x + arg2, pos.y + arg1, 0);
                break;
            case 'S':
                next = new Vector3Int(pos.x + arg2, pos.y - arg1, 0);
                break;
            case 'E':
                next = new Vector3Int(pos.x + arg1, pos.y + arg2, 0);
                break;
            default: //W
                next = new Vector3Int(pos.x - arg1, pos.y + arg2, 0);
                break;
        }

        return next;
    }
}
