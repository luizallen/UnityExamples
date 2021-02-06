using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [HideInInspector]
    public Text UnitName;
    [HideInInspector]
    public Text LVL;
    [HideInInspector]
    public Text HP;
    [HideInInspector]
    public Text MP;
    [HideInInspector]
    public Text ACC;
    [HideInInspector]
    public Text EVD;
    [HideInInspector]
    public Text RES;
    [HideInInspector]
    public Text CT;
    [HideInInspector]
    public Image Portrait;

    [HideInInspector]
    public PanelPositioner PanelPositioner;

    void Awake()
    {
        PanelPositioner = GetComponent<PanelPositioner>();

        UnitName = transform.Find("UnitName").GetComponent<Text>();
        LVL = transform.Find("LVL").GetComponent<Text>();
        HP = transform.Find("HP").GetComponent<Text>();
        MP = transform.Find("MP").GetComponent<Text>();
        ACC = transform.Find("ACC").GetComponent<Text>();
        EVD = transform.Find("EVD").GetComponent<Text>();
        RES = transform.Find("RES").GetComponent<Text>();
        CT = transform.Find("CT").GetComponent<Text>();

        Portrait = transform.Find("Portrait").GetComponent<Image>();
    }

    public void Show(Unit unit)
    {
        SetValues(unit);
        PanelPositioner.MoveTo("Show");
    }

    public void Hide() => PanelPositioner.MoveTo("Hide");

    void SetValues(Unit unit)
    {
        UnitName.text = unit.name;

        LVL.text = "Lv. " + unit.Stats[StatEnum.LVL].CurrentValue;
        HP.text = string.Format("HP: {0}/{1}", unit.Stats[StatEnum.HP].CurrentValue, unit.Stats[StatEnum.MAXHP].CurrentValue);
        MP.text = string.Format("MP: {0}/{1}", unit.Stats[StatEnum.MP].CurrentValue, unit.Stats[StatEnum.MAXMP].CurrentValue);
        ACC.text = "ACC: " + unit.Stats[StatEnum.ACC].CurrentValue;
        EVD.text = "EVD: " + unit.Stats[StatEnum.EVD].CurrentValue;
        RES.text = "RES: " + unit.Stats[StatEnum.RES].CurrentValue;
        CT.text = "CT: " + unit.ChargeTime;
        Portrait.sprite = unit.Job.Portrait;
    }
}
