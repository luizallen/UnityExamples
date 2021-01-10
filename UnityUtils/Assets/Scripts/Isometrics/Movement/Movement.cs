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

    public IEnumerator Move(List<TileLogic> path)
    {
        _actualTile = Turn.Unit.Tile;

        for (int i = 0; i < path.Count; i++)
        {
            var to = path[i];
            if (_actualTile.Floor != to.Floor)
            {
                yield return StartCoroutine(Jump(to));
            }
            else
            {
                yield return StartCoroutine(Walk(to));
            }
        }
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
        to.content = this.gameObject;
    }

    IEnumerator Jump(TileLogic to)
    {
        int id1 = LeanTween.move(transform.gameObject, to.WorldPos, MoveSpeed).id;
        LeanTween.moveLocalY(_jumper.gameObject, JumpHeight, MoveSpeed * 0.5f).
        setLoopPingPong(1).setEase(LeanTweenType.easeInOutQuad);

        float timerOrderUpdate = MoveSpeed;
        if (_actualTile.Floor.Tilemap.tileAnchor.y > to.Floor.Tilemap.tileAnchor.y)
        {
            timerOrderUpdate *= 0.85f;
        }
        else
        {
            timerOrderUpdate *= 0.2f;
        }
        yield return new WaitForSeconds(timerOrderUpdate);
        _actualTile = to;
        _sR.sortingOrder = to.ContentOrder;

        while (LeanTween.descr(id1) != null)
        {
            yield return null;
        }
        to.content = this.gameObject;
    }
}
