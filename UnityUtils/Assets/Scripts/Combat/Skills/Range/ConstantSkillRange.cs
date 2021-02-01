using System;
using System.Collections.Generic;

public class ConstantSkillRange : SkillRange
{
    public override List<TileLogic> GetTileInRange()
    {
        return Board.Instance.Search(Turn.Unit.Tile, SearchType);
    }

    bool SearchType(TileLogic from, TileLogic to)
    {
        to.Distance = from.Distance + 1;

        return (from.Distance + 1) <= Range &&
            Math.Abs(to.Height - Turn.Unit.Tile.Height) <= VerticalRange;
    }
}
