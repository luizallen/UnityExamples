using UnityEngine;

namespace Assets.Scripts.Isometrics.State_Machine.States
{
    public class ChooseActionState : State
    {
        int index;

        public override void Enter()
        {
            MoveSelector(Turn.unit.tile);
            base.Enter();

            index = 0;
            ChangeUISelector();

            inputs.OnMove += OnMove;
            inputs.OnFire += OnFire;

            stateMachine.chooseActionPanel.MoveTo("Show");
        }

        public override void Exit()
        {
            base.Exit();

            inputs.OnMove -= OnMove;
            inputs.OnFire -= OnFire;

            stateMachine.chooseActionPanel.MoveTo("Hide");
        }

        void OnMove(object sender, object args)
        {
            var button = (Vector3Int)args;

            if (button == Vector3Int.left)
            {
                index--;
                ChangeUISelector();
            }
            else if (button == Vector3Int.right)
            {
                index++;
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
                stateMachine.ChangeTo<RoamState>();
            }
        }

        void ChangeUISelector()
        {
            if(index == -1)
            {
                index = stateMachine.chooseActionButtons.Count - 1;
            }else if(index  == stateMachine.chooseActionButtons.Count)
            {
                index = 0;
            }

            stateMachine.chooseActionSelection.transform.localPosition =
                stateMachine.chooseActionButtons[index].transform.localPosition;
        }

        void ActionButtons()
        {
            Debug.Log(index);

            switch (index)
            {
                case 0:
                    //stateMachine.ChangeTo<MoveTargetState>();
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
