using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static GameOverManager instance;

    private void Awake()
    {
        // pour v�rifier qu'il n'y a qu'une seule instance de GameOverManager dans la sc�ne
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Attention, il y a plus d'une instance de GameOverManager dans la sc�ne");
            Destroy(gameObject);
        }
    }

    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
    }

    public void RetryButton()
    {
        // pour recharger la sc�ne
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // pour remettre son compteur de diamant � 0
        Inventory.instance.RemoveDiamonds(CurrentSceneManager.instance.diamondsPickedUpInThisSceneCount);

        // pour replacer le joueur au spawn
        PlayerLife.instance.Respawn();

        // pour r�activer les mouvements du joueur
        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
