using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPositioner : MonoBehaviour
{
    public List<PanelPosition> Positions;
    RectTransform _rect;

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void MoveTo(string positionName)
    {
        StopAllCoroutines();
        LeanTween.cancel(this.gameObject);

        var pos = Positions.Find(x => x.Name == positionName);

        StartCoroutine(Move(pos));
    }

    IEnumerator Move(PanelPosition panelPosition)
    {
        _rect.anchorMax = panelPosition.AnchorMax;
        _rect.anchorMin = panelPosition.AnchorMin;

        int id = LeanTween.move(_rect, panelPosition.Position, 0.5f).id;

        while(LeanTween.descr(id) != null)
        {
            yield return null;
        }
    }
}
