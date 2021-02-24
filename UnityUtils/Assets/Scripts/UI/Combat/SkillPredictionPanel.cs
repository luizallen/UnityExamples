using UnityEngine;
using UnityEngine.UI;

public class SkillPredictionPanel : MonoBehaviour
{
    [HideInInspector]
    public Text SkillName;
    [HideInInspector]
    public Text ChanceToHit;
    [HideInInspector]
    public Text PredictEffect;

    [HideInInspector]
    public PanelPositioner positioner;

    void Awake()
    {
        positioner = GetComponent<PanelPositioner>();
        SkillName = transform.Find("SkillName").GetComponent<Text>();
        ChanceToHit = transform.Find("HitChance").GetComponent<Text>();
        PredictEffect = transform.Find("PredictEffect").GetComponent<Text>();
    }

    public void SetPredictionText()
    {
        SkillName.text = Turn.Skill.name;

        var toHit = 0;
        var predict = 0;
        Unit target = null;

        foreach (var tileLogic in Turn.Targets)
        {
            if (tileLogic.content != null)
            {
                target = tileLogic.content.GetComponent<Unit>();
                if (target != null)
                    break;
            }
        }

        if (target != null)
        {
            toHit = Turn.Skill.GetHitPrediction(target);
            predict = Turn.Skill.GetEffectPrediction(target);
            CombatStateMachineController.Instance.RightCharacterPanel.Show(target);
        }

        ChanceToHit.text = Mathf.Clamp(toHit, 0, 100) + "% chance to hit";
        PredictEffect.text = predict + " hit points";
    }
}
