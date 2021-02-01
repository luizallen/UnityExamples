using System.Collections.Generic;

public class SingleAOE : AreaOfEffect
{
    public override List<TileLogic> GetArea(List<TileLogic> tiles) => new List<TileLogic> { Selector.Instance.Tile };
}