using Photon.Pun;
using UnityEngine;

public class ServerSelectionState : MenuUIState
{
    public override void Enter()
    {
        StateMachine.ServerSelectionPanel.Show();
        StateMachine.ServerSelectionPanel.EnterButton.onClick.AddListener(Login);
        StateMachine.ServerSelectionPanel.LogoutButton.onClick.AddListener(Logout);

        Inputs.OnMove += OnMove;
    }

    public override void Exit()
    {
        StateMachine.ServerSelectionPanel.Hide();
        Inputs.OnMove -= OnMove;
    }

    void Login()
    {
        NetworkController.Instance.Connect();
    }

    void Logout()
    {
        PhotonNetwork.Disconnect();
        StateMachine.ChangeTo<LoginState>();
    }

    void OnMove(object sender, object args)
    {
        var button = (Vector3Int)args;

        if (button == Vector3Int.down)
        {
            StateMachine.ServerSelectionPanel.SelectNext();
        }
        else if (button == Vector3Int.up)
        {
            StateMachine.ServerSelectionPanel.SelectPrevious();
        }
    }
}
