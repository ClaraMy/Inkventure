using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject m_GameOverUI;

    /// <summary>
    /// Gets the singleton instance of the GameOverManager.
    /// </summary>
    #region Singleton
    public static GameOverManager Instance;

    private void Awake()
    {
        // Check if there is more than one instance in the scene
        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of GameOverManager in the scene");
            return;
        }

        Instance = this;
    }
    #endregion

    /// <summary>
    /// Activates the game over UI.
    /// </summary>
    public void OnPlayerDeath()
    {
        m_GameOverUI.SetActive(true);
    }

    /// <summary>
    /// Restarts the game when the player chooses to retry.
    /// </summary>
    public void Retry()
    {
        // Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Resets the diamond count to zero
        Inventory.Instance.RemoveDiamonds(CurrentSceneManager.Instance.DiamondsPickedUpInThisSceneCount);

        // Respawns the player at the spawn point
        PlayerLife.Instance.Respawn();

        // Reactivates player movements
        m_GameOverUI.SetActive(false);
    }

    /// <summary>
    /// Return to the main menu.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Quit the game.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
