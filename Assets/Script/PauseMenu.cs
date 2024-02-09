using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    private void Start()
    {
        gameIsPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    private void Paused()
    {
        // pour afficher le menu pause
        pauseMenuUI.SetActive(true);

        // pour arrêter le temps
        Time.timeScale = 0;

        // pour changer le statut du jeu
        gameIsPaused = true;

        // pour arrêter les mouvements du joueur
        PlayerMovement.instance.enabled = false;
    }

    public void Resume()
    {
        // pour retirer le menu pause
        pauseMenuUI.SetActive(false);

        // pour remettre le temps normal
        Time.timeScale = 1;

        // pour changer le statut du jeu
        gameIsPaused = false;

        // pour remettre les mouvements du joueur
        PlayerMovement.instance.enabled = true;
    }

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
