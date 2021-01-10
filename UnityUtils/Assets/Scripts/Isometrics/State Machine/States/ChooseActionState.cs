using UnityEngine;

namespace Assets.Scripts.Isometrics.State_Machine.States
{
    public class ChooseActionState : State
    {
        int _index;

        public override void Enter()
        {
            MoveSelector(Turn.Unit.Tile);
            base.Enter();

            _index = 0;
            ChangeUISelector();

            Inputs.OnMove += OnMove;
            Inputs.OnFire += OnFire;

            StateMachine.chooseActionPanel.MoveTo("Show");
        }

        public override void Exit()
        {
            base.Exit();

            Inputs.OnMove -= OnMove;
            Inputs.OnFire -= OnFire;

            StateMachine.chooseActionPanel.MoveTo("Hide");
        }

        void OnMove(object sender, object args)
        {
            var button = (Vector3Int)args;

            if (button == Vector3Int.left)
            {
                _index--;
                ChangeUISelector();
            }
            else if (button == Vector3Int.right)
            {
                _index++;
                ChangeUISelector();
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

        void ChangeUISelector()
        {
            if(_index == -1)
            {
                _index = StateMachine.chooseActionButtons.Count - 1;
            }else if(_index  == StateMachine.chooseActionButtons.Count)
            {
                _index = 0;
            }

            StateMachine.chooseActionSelection.transform.localPosition =
                StateMachine.chooseActionButtons[_index].transform.localPosition;
        }

        void ActionButtons()
        {
            Debug.Log(_index);

            switch (_index)
            {
                case 0:
                    StateMachine.ChangeTo<MoveSelectionState>();
                    break;
                case 1:
                    //stateMachine.ChangeTo<ActionSelectState>();
                    break;
                case 2:
                    //stateMachine.ChangeTo<ItemSelectState>();
                    break;
                case 3:
                    //stateMachine.ChangeTo<WaitState>();
                    break;
            }
        }
    }
}
