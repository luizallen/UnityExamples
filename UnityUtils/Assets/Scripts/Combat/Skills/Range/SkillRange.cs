using System.Collections.Generic;
using UnityEngine;

public abstract class SkillRange : MonoBehaviour
{
    public int Range;
    public int VerticalRange;

    public abstract List<TileLogic> GetTileInRange();

    public virtual bool IsDirectionOriented()
        => false;

    public virtual char GetDirection()
    {
        if (Turn.Targets[0] == Turn.Unit.Tile)
            return Turn.Unit.Direction;

        return Turn.Unit.Tile.GetDirection(Turn.Targets[0]);
    }
}
