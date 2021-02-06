using Assets.Scripts.Combat;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    public static CombatText Instance;
    public TextMeshProUGUI Prefab;
    public Vector3 OffSet;
    public Vector3 CustomRandom;
    public float TimeToLive; //ttl

    void Awake()
    {
        Instance = this;
    }

    public void PopText(Unit unit, int value) 
        => StartCoroutine(PopControl(unit, value));

    IEnumerator PopControl(Unit unit, int value)
    {
        var randomPos = new Vector3(Random.Range(-CustomRandom.x, CustomRandom.x), Random.Range(-CustomRandom.y, CustomRandom.y), 0);
        var customPosition = unit.transform.position + OffSet + randomPos;

        var instantiated = Instantiate(Prefab, customPosition, Quaternion.identity, unit.transform.Find("Panel"));

        instantiated.transform.SetAsLastSibling();

        if (value <= 0)
            instantiated.color = Color.red;
        else
            instantiated.color = Color.green;

        instantiated.text = Mathf.Abs(value).ToString();

        LeanTween.alphaCanvas(instantiated.GetComponent<CanvasGroup>(), 1, 0.5f);
        yield return new WaitForSeconds(TimeToLive);

        var endPos = new Vector3(customPosition.x, customPosition.y + 0.5f);

        LeanTween.move(instantiated.gameObject, endPos, 0.9f);
        LeanTween.alphaCanvas(instantiated.GetComponent<CanvasGroup>(), 0, 1)
            .setOnComplete(() => Destroy(instantiated.gameObject));
    }
}
