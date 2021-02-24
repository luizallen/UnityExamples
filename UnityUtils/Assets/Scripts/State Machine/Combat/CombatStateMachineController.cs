using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatStateMachineController : BaseStateMachine
{
    public static CombatStateMachineController Instance;
   
    public Transform Selector;
    public TileLogic SelectedTile;
    public List<Unit> Units;    

    [Header("ChooseActionState")]
    public List<Image> ChooseActionButtons;
    public Image ChooseActionSelection;
    public PanelPositioner ChooseActionPanel;

    [Header("SkillSelectionState")]
    public List<Image> SkillSelectionButtons;
    public Image SkillSelectionSelection;
    public PanelPositioner SkillSelectionPanel;
    public Sprite SkillSelectionBlocked;

    [Header("EndPanel")]
    public PanelPositioner EndPanel;
    public Text EndText;

    [Header("SKill Prediction")]
    public SkillPredictionPanel SkillPredictionPanel;
    public CharacterPanel LeftCharacterPanel;
    public CharacterPanel RightCharacterPanel;

    [Header("JobAdvance")]
    public JobAdvancePanel JobAdvancePanel;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeTo<LoadState>();
    }
}
