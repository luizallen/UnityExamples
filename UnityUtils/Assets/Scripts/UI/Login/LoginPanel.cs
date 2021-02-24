using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : PanelPositioner
{
    public TMP_InputField PlayerNameInput;
    public Button LoginButton;

    GameObject _this;

    void Awake()
    {
        _this = this.gameObject;
    }

    public void Show()
    {
        _this.SetActive(true);
    }

    public void Hide()
    {
        _this.SetActive(false);
    }
}
