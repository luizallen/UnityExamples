using Photon.Pun;
using UnityEngine;

public class ServerSelectionState : MenuUIState
{
    void Update()
    {
        if (StateMachine.Current.GetType() == typeof(ServerSelectionState)
           &&  PhotonNetwork.IsConnectedAndReady)
            StateMachine.LoadingPanel.Message.text = "Connected";
    }

    public override void Enter()
    {
        StateMachine.ServerSelectionPanel.Show();
        StateMachine.ServerSelectionPanel.EnterButton.onClick.AddListener(ConnectOnServer);
        StateMachine.ServerSelectionPanel.LogoutButton.onClick.AddListener(Logout);

        Inputs.OnMove += OnMove;
    }

    public override void Exit()
    {
        if (StateMachine.ServerSelectionPanel.isActiveAndEnabled)
            StateMachine.ServerSelectionPanel.Hide();

        StateMachine.LoadingPanel.Hide();
        Inputs.OnMove -= OnMove;
    }

    void ConnectOnServer()
    {
        NetworkController.Instance.ConnectOnServer();
        StateMachine.ServerSelectionPanel.Hide();
        StateMachine.LoadingPanel.Show("Connecting on server");
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
