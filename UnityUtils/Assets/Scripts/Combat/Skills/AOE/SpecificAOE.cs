using System;
using System.Collections.Generic;
using UnityEngine;

public class SpecificAOE : AreaOfEffect
{
    public int Range;
    public int VerticalRange;

    TileLogic _tile;

    public override List<TileLogic> GetArea(List<TileLogic> tiles)
    {
        _tile = Selector.Instance.Tile;
        return Board.Instance.Search(_tile, SearchType);
    }

    bool SearchType(TileLogic from, TileLogic to)
    {
        to.Distance = from.Distance + 1;

        return (from.Distance + 1) <= Range && Mathf.Abs(to.Height - _tile.Height) <= VerticalRange;
    }
}
