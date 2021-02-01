using System.Collections.Generic;

public class InfiniteSkillRange : SkillRange
{
    public override List<TileLogic> GetTileInRange()
    {
        return new List<TileLogic>(Board.Instance.Tiles.Values);
    }
}
