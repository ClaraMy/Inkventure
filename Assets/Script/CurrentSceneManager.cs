using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public int LevelToUnlock;
    public int DiamondsPickedUpInThisSceneCount;

    /// <summary>
    /// Gets the singleton instance of the Inventory.
    /// </summary>
    #region Singleton
    public static CurrentSceneManager Instance;

    private void Awake()
    {
        // Check if there is more than one instance in the scene
        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of CurrentSceneManager in the scene");
            return;
        }

        Instance = this;
    }
    #endregion
}
