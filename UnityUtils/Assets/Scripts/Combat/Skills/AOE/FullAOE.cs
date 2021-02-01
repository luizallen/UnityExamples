using System.Collections.Generic;

public class FullAOE : AreaOfEffect
{
    public override List<TileLogic> GetArea(List<TileLogic> tiles) => tiles;
}
