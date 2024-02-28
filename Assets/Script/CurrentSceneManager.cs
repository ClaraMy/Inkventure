using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public int LevelToUnlock;
    public int DiamondsPickedUpInThisSceneCount;

    /// <summary>
    /// Gets the singleton instance of the Inventory.
    /// </summary>
    #region Singleton
    private static CurrentSceneManager m_Instance;

    public static CurrentSceneManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new CurrentSceneManager();
            }

            return m_Instance;
        }
    }
    #endregion
}
