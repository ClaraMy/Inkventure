using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_SettingsWindow;

    /// <summary>
    /// Starts the game by loading the level selection scene.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    /// <summary>
    /// Displays the settings window.
    /// </summary>
    public void Settings()
    {
        m_SettingsWindow.SetActive(true);
    }

    /// <summary>
    /// Closes the settings window.
    /// </summary>
    public void CloseSettingsWindow()
    {
        m_SettingsWindow.SetActive(false);
    }

    /// <summary>
    /// Quit the game.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
