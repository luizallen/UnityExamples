using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float MoveSpeed = 0.5f;
    const float JumpHeight = 0.5f;
    SpriteRenderer _sR;
    Transform _jumper;
    TileLogic _actualTile;

    void Awake()
    {
        _jumper = transform.Find("Jumper");
        _sR = GetComponentInChildren<SpriteRenderer>();
    }

    public IEnumerator Move(TileLogic currentTile, TileLogic fowardTile, Unit unit)
    {
        var path = CreatePath(currentTile, fowardTile);
        yield return Move(path, unit);
    }

    public IEnumerator Move(TileLogic tileLogic, Unit unit)
    {
        var path = new List<TileLogic> { tileLogic };
        yield return Move(path, unit);
    }

    public IEnumerator Move(List<TileLogic> path, Unit unit)
    {
        _actualTile = unit.Tile;
        _actualTile.content = null;

        for (int i = 0; i < path.Count; i++)
        {
            var to = path[i];

            unit.Direction = _actualTile.GetDirection(to);
            
            if (_actualTile.Floor != to.Floor)
            {
                var duration = unit.AnimationController.Jump();
                yield return StartCoroutine(Jump(to, duration));
            }
            else
            {
                unit.AnimationController.Walk();
                yield return StartCoroutine(Walk(to));
            }

            unit.Tile = to;
        }

        unit.AnimationController.Idle();
    }

    IEnumerator Walk(TileLogic to)
    {
        int id = LeanTween.move(transform.gameObject, to.WorldPos, MoveSpeed).id;
        _actualTile = to;

        yield return new WaitForSeconds(MoveSpeed * 0.5f);
        _sR.sortingOrder = to.ContentOrder;

        while (LeanTween.descr(id) != null)
        {
            yield return null;
        }
    }

    IEnumerator Jump(TileLogic to, float duration)
    {
        yield return new WaitForSeconds(0.15f);

        float timerOrderUpdate = duration;
        if (_actualTile.Floor.Tilemap.tileAnchor.y > to.Floor.Tilemap.tileAnchor.y)
        {
            timerOrderUpdate *= 0.85f;
        }
        else
        {
            timerOrderUpdate *= 0.2f;
        }

        int id1 = LeanTween.move(transform.gameObject, to.WorldPos, duration).id;
        LeanTween.moveLocalY(_jumper.gameObject, JumpHeight, duration * 0.5f).
        setLoopPingPong(1).setEase(LeanTweenType.easeInOutQuad);

        yield return new WaitForSeconds(timerOrderUpdate);
        _actualTile = to;
        _sR.sortingOrder = to.ContentOrder;

        while (LeanTween.descr(id1) != null)
        {
            yield return null;
        }
    }

    public virtual bool ValidateMovement(TileLogic from, TileLogic to)
        => IsValidMovement(from, to, Turn.Unit.GetStat(StatEnum.MOV));

    public virtual bool ValidateGlobalMovement(TileLogic from, TileLogic to)
        => IsValidMovement(from, to, 99);

    bool IsValidMovement(TileLogic from, TileLogic to, int movement)
    {
        to.Distance = from.Distance + 1;

        if (to.content != null
            || to.Distance > movement
            || Mathf.Abs(from.Floor.Height - to.Floor.Height) > 1)
            return false;

        return true;
    }

    private List<TileLogic> CreatePath(TileLogic currentTile, TileLogic fowardTile)
    {
        var path = new List<TileLogic>();

        var tile = fowardTile;

        while (tile != currentTile)
        {
            path.Add(tile);
            tile = tile.Prev;
        }

        path.Reverse();
        return path;
    }
}