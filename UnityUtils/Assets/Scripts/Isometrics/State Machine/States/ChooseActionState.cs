namespace Assets.Scripts.Isometrics.State_Machine.States
{
    public class ChooseActionState : State
    {
        public override void Enter()
        {
            base.Enter();
            InputController.instance.OnMove += OnMove;
        }

        public override void Exit()
        {
            base.Exit();
            InputController.instance.OnMove -= OnMove;
        }

        void OnMove(object sender, object args)
        {
        }
    }
}
