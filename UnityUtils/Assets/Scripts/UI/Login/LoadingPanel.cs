using TMPro;
using UnityEngine;

public class LoadingPanel : PanelPositioner
{
    public TextMeshProUGUI Message;

    GameObject _this;

    void Awake()
    {
        _this = this.gameObject;
    }

    public void Show(string message)
    {
        Message.text = message;
        _this.SetActive(true);
    }

    public void Hide()
    {
        _this.SetActive(false);
        Message.text = "";
    }
}
