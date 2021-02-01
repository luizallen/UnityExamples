using System.Collections.Generic;

public class SelfSkillRange : SkillRange
{
    public override List<TileLogic> GetTileInRange()
    {
        var retValue = new List<TileLogic>();
        retValue.Add(Turn.Unit.Tile);
        return retValue;
    }

    public override char GetDirection() => Turn.Unit.Direction;
}
