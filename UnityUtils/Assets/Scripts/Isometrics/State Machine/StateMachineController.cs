using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachineController : MonoBehaviour
{
    public static StateMachineController Instance;
   
    public State Current { get { return _current; } }
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

    State _current;
    bool _busy;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeTo<LoadState>();
    }

    public void ChangeTo<T>() where T : State
    {
        State state = GetState<T>();
        if (_current != state)
            ChangeState(state);
    }

    public T GetState<T>() where T : State
    {
        T target = GetComponent<T>();
        if (target == null)
            target = gameObject.AddComponent<T>();
        return target;
    }

    protected void ChangeState(State value)
    {
        if (_busy)
            return;
        _busy = true;

        if (_current != null)
        {
            _current.Exit();
        }

        _current = value;
        if (_current != null)
            _current.Enter();

        _busy = false;
    }

}
