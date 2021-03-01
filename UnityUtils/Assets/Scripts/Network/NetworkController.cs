using Assets.Scripts.Network;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public string VersionName = "0.1";
    public static NetworkController Instance;

    void Awake()
    {
        PhotonNetwork.Disconnect();

        Instance = this;   
        PhotonNetwork.AutomaticallySyncScene = true;        
    }

    public void Connect()
    {        
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = NetworkConfig.PlayerName;
        PhotonNetwork.GameVersion = VersionName;
    }

    public void ConnectOnServer()
    {
        //PhotonNetwork.ConnectToRegion(NetworkConfig.ServerRegion);
        PhotonNetwork.JoinLobby();
    }

    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        NetworkConfig.IsConnected = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        NetworkConfig.IsConnected = false;
        Debug.Log("Disconnected");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected on Lobby");
        Debug.Log("Server: " + PhotonNetwork.CloudRegion + "Ping " + PhotonNetwork.GetPing());
        PhotonNetwork.JoinRoom($"{NetworkConfig.ServerRegion}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connected on Room");
        Debug.Log("Room Name: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Current Player in Room: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        var roomOptions = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.CreateRoom($"{NetworkConfig.ServerRegion}", roomOptions, TypedLobby.Default);
    }
}
