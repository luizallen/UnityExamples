using Assets.Scripts.Network;
using UnityEngine;

public class LoginState : MenuUIState
{
    string _playerName;

    public override void Enter()
    {
        _playerName = "Player" + Random.Range(1, 99999);
        StateMachine.LoginPanel.PlayerNameInput.text = _playerName;

        StateMachine.ServerSelectionPanel.Hide();
        StateMachine.LoginPanel.Show();
        StateMachine.LoginPanel.LoginButton.onClick.AddListener(Login);
    }

    public override void Exit() => StateMachine.LoginPanel.Hide();

    void Login()
    {
        var tempName = StateMachine.LoginPanel.PlayerNameInput.text;

        if(!string.IsNullOrEmpty(tempName))
            _playerName = tempName;

        NetworkConfig.PlayerName = _playerName;

        StateMachine.ChangeTo<ServerSelectionState>();
    }
}
