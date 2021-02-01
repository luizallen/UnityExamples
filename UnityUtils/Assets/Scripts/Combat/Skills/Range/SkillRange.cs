using System.Collections.Generic;
using UnityEngine;

public abstract class SkillRange : MonoBehaviour
{
    public int Range;
    public int VerticalRange;

    [HideInInspector]
    public bool DirectionOriented;

    public abstract List<TileLogic> GetTileInRange();

    public virtual char GetDirection()
        => Turn.Unit.Tile.GetDirection(Turn.Targets[0]);
}
