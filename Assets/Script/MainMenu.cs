using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_SettingsWindow;

    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Settings()
    {
        m_SettingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        m_SettingsWindow.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
