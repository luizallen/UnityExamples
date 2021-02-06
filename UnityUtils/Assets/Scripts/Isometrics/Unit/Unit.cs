using UnityEngine;
using UnityEngine.UI;

public delegate void OnTurnBegin();

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public Stats Stats;

    [HideInInspector]
    public AudioSource AudioSource;

    public int Faction;
    public int ChargeTime;
    public int Alliance;
    public string SpriteModel;
    public char Direction = 'S';
    public int Experience;

    public Job Job;
    public TileLogic Tile;
    public AnimationController AnimationController;
    public OnTurnBegin OnTurnBegin;
    public Image HealthBar;

    public SpriteSwapper SpriteSwapper;

    public bool _active;

    public bool Active
    {
        get { return _active; }
        set
        {
            if (_active && !value)
                GiveExp();

            _active = value;
        }
    }

    public bool Dead { get { return GetStat(StatEnum.HP) <= 0; } }

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        Stats = GetComponentInChildren<Stats>();
        SpriteSwapper = transform.Find("Jumper/Sprite").GetComponent<SpriteSwapper>();

        AnimationController = GetComponent<AnimationController>();
    }

    void Start()
    {
        SpriteSwapper.ThisUnitSprites = SpriteLoader.holder.Find(SpriteModel).GetComponent<SpriteLoader>();
        AnimationController.Idle();
    }

    public int GetStat(StatEnum stat) => Stats.StatsList[(int)stat].CurrentValue;

    public void SetStat(StatEnum stat, int value)
    {
        if (stat == StatEnum.HP)
        {
            Stats[stat].CurrentValue = ClampStat(StatEnum.MAXHP, Stats[stat].CurrentValue + value);
            PopCombatText(value);
            UpdateHealthBar();
        }
        else if (stat == StatEnum.MP)
        {
            Stats[stat].CurrentValue = ClampStat(StatEnum.MAXMP, Stats[stat].CurrentValue + value);
        }
    }

    public void RandomizeStats()
    {
        foreach (var stats in Stats.StatsList)
        {
            if (stats.Type == StatEnum.MOV)
            {
                stats.BaseValue = Random.Range(1, 5);
                continue;
            }

            var temp = Random.Range(1, 100); ;

            stats.BaseValue = temp;
            stats.CurrentValue = temp;
        }
    }

    public void UpdateStat(StatEnum value)
    {
        var toUpdate = Stats.StatsList[(int)value];
        toUpdate.CurrentValue = Stats[value].BaseValue;

        if (toUpdate.Modifiers != null)
            toUpdate.Modifiers(toUpdate);
    }

    public void UpdateStat()
    {
        foreach (var stat in Stats.StatsList)
        {
            UpdateStat(stat.Type);
        }
    }

    void UpdateHealthBar()
    {
        float maxHP = GetStat(StatEnum.MAXHP);
        float fillValue = (Stats[StatEnum.HP].CurrentValue * 100 / maxHP) / 100;
        HealthBar.fillAmount = fillValue;

        if (fillValue >= 0.7f)
            HealthBar.color = Color.green;
        else if (fillValue >= 0.3f)
            HealthBar.color = Color.yellow;
        else
            HealthBar.color = Color.red;
    }

    int ClampStat(StatEnum type, int value)
        => Mathf.Clamp(value, 0, Stats[type].CurrentValue);

    void PopCombatText(int value)
        => CombatText.Instance.PopText(this, value);

    void GiveExp()
    {
        foreach (var unit in StateMachineController.Instance.Units)
        {
            if(unit.Alliance != Alliance)
            {
                unit.Experience += 1000;
                Job.CheckLevelUp(unit);
            }
        }
    }
}
