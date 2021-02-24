using UnityEngine;

public class State : MonoBehaviour
{
    protected InputController Inputs { get { return InputController.Instance; } }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }   
}
