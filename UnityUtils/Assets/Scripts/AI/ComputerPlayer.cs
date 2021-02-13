using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{
    public static ComputerPlayer Instance;

    Unit CurrentUnit { get => Turn.Unit; }
    int Alliance { get => CurrentUnit.Alliance; }

    Unit NearestFoe;

    public AIPlan CurrentPlan;

    void Awake()
    {
        Instance = this;
    }

    public AIPlan Evaluate()
    {
        var aiSkillBehavior = Turn.Unit.GetComponent<AISkillBehavior>();

        if (aiSkillBehavior == null)
            aiSkillBehavior = Turn.Unit.gameObject.AddComponent<AISkillBehavior>();

        var plan = new AIPlan();
        if (TryToEvaluate(plan, aiSkillBehavior))
        {
            Debug.Log("Achou algo de bom pra fazer");
        }

        if (plan.Skill == null)
            MoveTowardOpponent(plan);

        CurrentPlan = plan;
        return plan;
    }

    bool TryToEvaluate(AIPlan plan, AISkillBehavior aISkillBehavior)
    {
        aISkillBehavior.Pick(plan);

        if (plan.Skill == null)
            return false;

        if (IsDirectionIndependent(plan))
            PlanDirectionIndependent(plan);
        else
            PlanDirectionDependent(plan);

        return true;
    }

    void FindNearestFoe()
    {
        NearestFoe = null;
        Board.Instance.Search(Turn.Unit.Tile, (TileLogic arg1, TileLogic arg2) =>
        {
            if (NearestFoe == null && arg2.content != null)
            {
                var unit = arg2.content.GetComponent<Unit>();
                if (unit != null && CurrentUnit.Alliance != unit.Alliance)
                {
                    var stats = unit.Stats;
                    if (stats[StatEnum.HP].CurrentValue > 0)
                    {
                        NearestFoe = unit;
                        return true;
                    }
                }
            }

            arg2.Distance = arg1.Distance + 1;
            return NearestFoe == null;
        });
    }

    List<TileLogic> GetMoveOptions() => Board.Instance.Search(Turn.Unit.Tile, Turn.Unit.GetComponent<Movement>().ValidateMovement);

    List<TileLogic> GetMoveOptions(bool includeCurrentPosition)
    {
        var result = GetMoveOptions();
        result.Add(Turn.Unit.Tile);

        return result;
    }

    void MoveTowardOpponent(AIPlan plan)
    {
        var moveOptions = GetMoveOptions();
        FindNearestFoe();

        if (NearestFoe != null)
        {
            var toCheck = NearestFoe.Tile;
            while (toCheck != null)
            {
                if (moveOptions.Contains(toCheck))
                {
                    plan.MovePos = toCheck.Pos;
                    return;
                }

                toCheck = toCheck.Prev;
            }
        }

        plan.MovePos = Turn.Unit.Tile.Pos;
    }

    bool IsDirectionIndependent(AIPlan plan) => !plan.Skill.GetComponentInChildren<SkillRange>().IsDirectionOriented();

    void PlanDirectionDependent(AIPlan plan)
    {
        var startTile = Turn.Unit.Tile;
        var startDirection = Turn.Unit.Direction;
        var attackOptions = new List<AttackOption>();
        var moveOptions = GetMoveOptions(true);
        var selectorStartTile = Selector.Instance.Tile;

        var directions = new char[] { 'N', 'S', 'E', 'W' };

        foreach (var moveOption in moveOptions)
        {
            var moveTile = moveOption;
            Turn.Unit.Tile.content = null;
            Turn.Unit.Tile = moveTile;
            moveTile.content = Turn.Unit.gameObject;

            foreach (var direction in directions)
            {
                Turn.Unit.Direction = direction;
                var attackOption = new AttackOption();
                attackOption.Target = moveTile;
                attackOption.Direction = Turn.Unit.Direction;

                RateFireLocation(plan, attackOption);
                attackOption.MoveTargets.Add(moveTile);
                attackOptions.Add(attackOption);
            }
        }

        Turn.Unit.Tile.content = null;
        Turn.Unit.Tile = startTile;
        startTile.content = Turn.Unit.gameObject;
        Turn.Unit.Direction = startDirection;
        Selector.Instance.Tile = selectorStartTile;

        PickBestOption(plan, attackOptions);
    }

    void PlanDirectionIndependent(AIPlan plan)
    {
        var startTile = Turn.Unit.Tile;
        var map = new Dictionary<TileLogic, AttackOption>();
        var skillRange = plan.Skill.GetComponentInChildren<SkillRange>();
        var moveOptions = GetMoveOptions(true);
        var selectorStartTile = Selector.Instance.Tile;

        foreach (var moveOption in moveOptions)
        {
            Turn.Unit.Tile.content = null;
            Turn.Unit.Tile = moveOption;
            moveOption.content = Turn.Unit.gameObject;

            var fireOptions = skillRange.GetTileInRange();
            foreach (var fireOption in fireOptions)
            {
                AttackOption attackOption;

                if (map.ContainsKey(fireOption))
                    attackOption = map[fireOption];
                else
                {
                    attackOption = new AttackOption();
                    map[fireOption] = attackOption;
                    attackOption.Target = fireOption;
                    attackOption.Direction = Turn.Unit.Direction;
                    RateFireLocation(plan, attackOption);
                }

                attackOption.MoveTargets.Add(moveOption);
            }
        }

        Turn.Unit.Tile.content = null;
        Turn.Unit.Tile = startTile;
        startTile.content = Turn.Unit.gameObject;
        var attackOptions = new List<AttackOption>(map.Values);
        Selector.Instance.Tile = selectorStartTile;

        PickBestOption(plan, attackOptions);
    }

    bool IsSkillTargetMatch(AIPlan plan, TileLogic tile, Unit unit)
    {
        switch (plan.TargetType)
        {
            case SkillAffectsType.Default:
                return true;
            case SkillAffectsType.EnemyOnly:
                if (unit.Alliance != Turn.Unit.Alliance)
                    return true;
                break;
            case SkillAffectsType.AllyOnly:
                if (unit.Alliance == Turn.Unit.Alliance)
                    return true;
                break;
        }

        return false;
    }

    void RateFireLocation(AIPlan plan, AttackOption attackOption)
    {
        var tiles = plan.Skill.GetTargets();
        var area = plan.Skill.GetComponentInChildren<AreaOfEffect>();

        Selector.Instance.Tile = attackOption.Target;

        tiles = area.GetArea(tiles);

        attackOption.AreaTargets = tiles;
        attackOption.IsCasterMatch = IsSkillTargetMatch(plan, Turn.Unit.Tile, Turn.Unit);

        foreach (var tile in tiles)
        {
            if (Turn.Unit.Tile == tile || tile.content == null)
                continue;

            var unit = tile.content.GetComponent<Unit>();
            if (unit == null || unit.Dead)
                continue;

            var isMatch = IsSkillTargetMatch(plan, tile, unit);
            attackOption.AddHit(tile, isMatch);
        }
    }

    void PickBestOption(AIPlan plan, List<AttackOption> attackOptions)
    {
        var bestScore = 1;
        var bestOptions = new List<AttackOption>();

        foreach (var option in attackOptions)
        {
            var score = option.GetScore(Turn.Unit, plan.Skill);
            if (score > bestScore)
            {
                bestScore = score;
                bestOptions.Clear();
                bestOptions.Add(option);
            }
            else if (score == bestScore)
                bestOptions.Add(option);
        }

        if (bestOptions.Count == 0)
        {
            plan.Skill = null;
            return;
        }

        var choice = bestOptions[UnityEngine.Random.Range(0, bestOptions.Count)];
        plan.SkillTargetPos = choice.Target.Pos;
        plan.Direction = choice.Direction;
        plan.MovePos = choice.BestMoveTile.Pos;
    }
}
