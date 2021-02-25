using Assets.Scripts.Network;
using Photon.Pun;
using UnityEngine;

public class LoginState : MenuUIState
{
    string _playerName;

    void Update()
    {
        if (StateMachine.Current.GetType() == typeof(LoginState)
           && NetworkConfig.IsConnected)
            StateMachine.ChangeTo<ServerSelectionState>();
    }

    public override void Enter()
    {
        _playerName = "Player" + Random.Range(1, 99999);
        StateMachine.LoginPanel.PlayerNameInput.text = _playerName;

        StateMachine.ServerSelectionPanel.Hide();
        StateMachine.LoadingPanel.Hide();

        StateMachine.LoginPanel.Show();
        StateMachine.LoginPanel.LoginButton.onClick.AddListener(Login);
    }

    public override void Exit()
    {
        if (StateMachine.LoginPanel.isActiveAndEnabled)
            StateMachine.LoginPanel.Hide();

        StateMachine.LoadingPanel.Hide();
    }

    void Login()
    {
        var tempName = StateMachine.LoginPanel.PlayerNameInput.text;

        if (!string.IsNullOrEmpty(tempName))
            _playerName = tempName;

        NetworkConfig.PlayerName = _playerName;
        NetworkController.Instance.Connect();
        StateMachine.LoginPanel.Hide();
        StateMachine.LoadingPanel.Show("Connecting");
    }
}
