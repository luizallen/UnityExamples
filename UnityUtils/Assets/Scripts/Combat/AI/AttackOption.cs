using System.Collections.Generic;
using UnityEngine;

public class AttackOption 
{
    class Hit {
        public TileLogic Tile;
        public bool IsMatch;

        public Hit(TileLogic tile, bool isMatch)
        {
            Tile = tile;
            IsMatch = isMatch;
        }
    }

    public TileLogic Target;
    public char Direction;
    public List<TileLogic> AreaTargets = new List<TileLogic>();
    public bool IsCasterMatch;
    public TileLogic BestMoveTile;
    public List<TileLogic> MoveTargets = new List<TileLogic>();

    List<Hit> _hits = new List<Hit>();

    public void AddHit(TileLogic tile, bool isMatch)
        => _hits.Add(new Hit(tile, isMatch));

    public int GetScore(Unit caster, Skill skill)
    {
        GetBestMoveTarget(caster, skill);

        if (BestMoveTile == null)
            return 0;

        var score = 0;

        foreach (var hit in _hits)
        {
            if (hit.IsMatch)
                score++;
            else
                score--;
        }

        if (IsCasterMatch && AreaTargets.Contains(BestMoveTile))
            score++;

        return score;
    }

    void GetBestMoveTarget(Unit caster, Skill skill)
    {
        if (MoveTargets.Count == 0)
            return;

        BestMoveTile = MoveTargets[Random.Range(0, MoveTargets.Count)];
    }
}
