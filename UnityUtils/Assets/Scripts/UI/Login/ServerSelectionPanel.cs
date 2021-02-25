using Assets.Scripts.Network;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ServerSelectionPanel : PanelPositioner
{
    int _index;

    [Header("Interaction Buttons")]
    public Button EnterButton;
    public Button LogoutButton;

    [Header("Server Selection List")]
    public Image Selector;
    public GameObject ServerListContent;
    public GameObject ServerListItemPrefab;

    public List<GameObject> InstantiatedServers = new List<GameObject>();

    GameObject _this;

    readonly Dictionary<string, string> _serversList = new Dictionary<string, string>
    {
        { "sa", "South America" },
        { "eu", "Europe" },   
        { "usw", "US West" },
    };

    void Awake()
    {
        _this = this.gameObject;
    }

    public void Show()
    {
        _index = 0;
        LoadServerAvailables();
        NetworkConfig.ServerRegion = _serversList.First().Key;

        _this.SetActive(true);
    }

    public void Hide()
    {
        ClearServerList();
        _this.SetActive(false);
    }

    public void SelectNext()
    {
        _index++;
        if (_index >= InstantiatedServers.Count)
            _index = 0;

        ChangeSelected();
    }

    public void SelectPrevious()
    {
        _index--;
        if (_index < 0)
            _index = InstantiatedServers.Count - 1;

        ChangeSelected();
    }

    void ChangeSelected()
    {
        var currentSelected = InstantiatedServers[_index];
        var newPosition = new Vector3Int
        {
            x = (int)currentSelected.transform.position.x,
            y = (int)currentSelected.transform.position.y - 10,
        };

        Selector.transform.position = newPosition;

        var serverItem = currentSelected.GetComponent<ServerItem>();

        NetworkConfig.ServerRegion = serverItem.ServerRegion;
    }

    void LoadServerAvailables()
    {
        ClearServerList();
        var count = 0;

        foreach (var server in _serversList)
        {
            var serverItem = Instantiate(ServerListItemPrefab,
                    ServerListContent.transform.position, 
                    Quaternion.identity,
                    ServerListContent.transform);

            var serverItemComponent = serverItem.GetComponent<ServerItem>();

            serverItemComponent.ServerName.text = server.Value;
            serverItemComponent.TotalOfPlayers.text = $"{0} / 20";
            serverItemComponent.ServerRegion = server.Key;

            var image = serverItem.GetComponent<Image>();

            if (count % 2 == 00)
                image.color = Color.white;
            else
                image.color = Color.gray;

            InstantiatedServers.Add(serverItem);
            count++;
        }
    }

    void ClearServerList()
    {
        foreach (var item in InstantiatedServers)
        {
            Destroy(item);
        }

        InstantiatedServers.Clear();
    }
}
