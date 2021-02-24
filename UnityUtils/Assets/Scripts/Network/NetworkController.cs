using Assets.Scripts.Network;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public static NetworkController Instance;

    private string _playerName;

    void Awake()
    {
        Instance = this;
    }

    public void Connect()
    {
        PhotonNetwork.NickName = NetworkConfig.PlayerName;
        PhotonNetwork.ConnectToRegion(NetworkConfig.ServerRegion);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinLobby();
    }

    public override void OnConnected()
    {
        Debug.Log("OnConnected");
        Debug.Log("Server: " + PhotonNetwork.CloudRegion + "Ping " + PhotonNetwork.GetPing());
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnConnected " + cause);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected on Lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        var roomTemp = "Room" + Random.Range(1, 10000);
        PhotonNetwork.CreateRoom(roomTemp);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entered on Room");
    }
}
