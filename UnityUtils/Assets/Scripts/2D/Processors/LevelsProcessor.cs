using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsProcessor : MonoBehaviour
{

    public string sceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
