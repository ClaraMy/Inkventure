using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button[] m_LevelButtons;

    private void Start()
    {
        // Retrieve the level reached by the player from PlayerPrefs
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        // Loop through the level buttons
        for (int i = 0; i < m_LevelButtons.Length; i++)
        {
            // Disable buttons for levels beyond the reached level
            if (i+1 > levelReached)
            {
                m_LevelButtons[i].interactable = false;
            }
        }
    }

    /// <summary>
    /// Loads the specified level.
    /// </summary>
    /// <param name="levelName">Name of the level to load.</param>
    public void LoadLevelPassed(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    /// <summary>
    /// Return to the main menu.
    /// </summary>
    public void MainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }


}
