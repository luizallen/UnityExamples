using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    public Transform Selector;
    public TileLogic SelectedTile;

    public State Current { get { return _current; } }
    State _current;

    bool _busy;

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
