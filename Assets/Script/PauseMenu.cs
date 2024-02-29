using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool m_GameIsPaused = false;

    [SerializeField] private GameObject m_PauseMenuUI;

    private void Start()
    {
        // Ensure that the game starts in a non-paused state
        m_GameIsPaused = false;
    }

    /// <summary>
    /// Reads the pause input.
    /// </summary>
    /// <param name="context">The input context from the Input System.</param>
    public void ReadPauseInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Toggle between pausing and resuming the game
            if (m_GameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    private void Paused()
    {
        // Show the pause menu UI
        m_PauseMenuUI.SetActive(true);

        // Stop the time to pause the game
        Time.timeScale = 0;

        // Update the game pause status
        m_GameIsPaused = true;

        // Disable player movement and attack while paused
        PlayerMovement.Instance.enabled = false;
        PlayerAttack.Instance.enabled = false;
    }

    /// <summary>
    /// Resume the game.
    /// </summary>
    public void Resume()
    {
        // Hide the pause menu UI
        m_PauseMenuUI.SetActive(false);

        // Restore the normal time scale to resume the game
        Time.timeScale = 1;

        // Update the game pause status
        m_GameIsPaused = false;

        // Enable player movement and attack
        PlayerMovement.Instance.enabled = true;
        PlayerAttack.Instance.enabled = true;
    }

    /// <summary>
    /// Return to the main menu.
    /// </summary>
    public void MainMenu()
    {
        // Resume the game before loading the main menu scene
        Resume();

        // Load the main menu scene
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
