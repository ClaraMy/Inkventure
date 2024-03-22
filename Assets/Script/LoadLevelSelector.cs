using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelSelector : MonoBehaviour
{
    [SerializeField] private Animator m_FadeSystem;

    /// <summary>
    /// Called when a collider enters this trigger collider.
    /// </summary>
    /// <param name="collision">The collider that entered this trigger collider.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to an object tagged as "Player"
        if (collision.CompareTag("Player"))
        {
            // Start the coroutine to load the level selector scene
            StartCoroutine(LoadLevelSelect());
        }
    }

    /// <summary>
    /// Coroutine to load the level selector scene after saving game data and fading in.
    /// </summary>
    /// <returns>An IEnumerator used for coroutine execution.</returns>
    public IEnumerator LoadLevelSelect()
    {
        // Save the current level progress
        SaveData.Instance.SaveLevel();

        // Trigger the fade-in animation
        m_FadeSystem.SetTrigger("FadeIn");

        // Wait for a short duration for the fade-in animation to complete
        yield return new WaitForSeconds(1f);

        // Load the level select scene
        SceneManager.LoadScene("LevelSelect");
    }
}
