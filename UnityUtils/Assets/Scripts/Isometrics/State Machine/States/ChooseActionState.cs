using UnityEngine;

namespace Assets.Scripts.Isometrics.State_Machine.States
{
    public class ChooseActionState : UIState
    {
        public override void Enter()
        {
            MoveSelector(Turn.Unit.Tile);
            base.Enter();
            Index = 0;

            CurrentUISelector = StateMachine.ChooseActionSelection;

            ChangeUISelector(StateMachine.ChooseActionButtons);
            CheckActions();

            Inputs.OnMove += OnMove;
            Inputs.OnFire += OnFire;

            StateMachine.ChooseActionPanel.MoveTo("Show");
        }

        public override void Exit()
        {
            base.Exit();

            Inputs.OnMove -= OnMove;
            Inputs.OnFire -= OnFire;

            StateMachine.ChooseActionPanel.MoveTo("Hide");
        }

        void OnMove(object sender, object args)
        {
            var button = (Vector3Int)args;

            if (button == Vector3Int.left)
            {
                Index--;
                ChangeUISelector(StateMachine.ChooseActionButtons);
            }
            else if (button == Vector3Int.right)
            {
                Index++;
                ChangeUISelector(StateMachine.ChooseActionButtons);
            }
        }

        void OnFire(object sender, object args)
        {
            int button = (int)args;

            if (button == 1)
            {
                ActionButtons();
            }
            else if (button == 2)
            {
                StateMachine.ChangeTo<RoamState>();
            }
        }

        void ActionButtons()
        {
            switch (Index)
            {
                case 0:
                    if (!Turn.HasMoved)
                        StateMachine.ChangeTo<MoveSelectionState>();
                    break;
                case 1:
                    if (!Turn.HasActed)
                        StateMachine.ChangeTo<SkillSelectionState>();
                    break;
                case 2:
                    StateMachine.ChangeTo<ItemSelectionState>();
                    break;
                case 3:
                    StateMachine.ChangeTo<TurnEndState>();
                    break;
            }
        }

        private void CheckActions()
        {
            PaintButton(StateMachine.ChooseActionButtons[0], Turn.HasMoved);
            PaintButton(StateMachine.ChooseActionButtons[1], Turn.HasActed);
            PaintButton(StateMachine.ChooseActionButtons[2], Turn.HasActed);
        }
    }
}
