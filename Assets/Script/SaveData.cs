using UnityEngine;

public class SaveData : MonoBehaviour
{
    /// <summary>
    /// Gets the singleton instance of the SaveData.
    /// </summary>
    #region Singleton
    public static SaveData Instance;

    private void Awake()
    {
        // Check if there is more than one instance in the scene
        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of SaveData in the scene");
            return;
        }

        Instance = this;
    }
    #endregion

    /// <summary>
    /// Saves the level progress.
    /// </summary>
    public void SaveLevel()
    {
        // Check if the level to unlock is greater than the current level reached
        if (CurrentSceneManager.Instance.LevelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            // Update the level reached in PlayerPrefs
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.Instance.LevelToUnlock);
        }
    }
}
