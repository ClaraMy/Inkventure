using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsWindow;

    public void StartGameButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
