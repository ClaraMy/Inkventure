using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private int m_DiamondsCount;

    [SerializeField] private Text m_DiamondsCountText;

    /// <summary>
    /// Gets the singleton instance of the Inventory.
    /// </summary>
    #region Singleton
    public static Inventory Instance;

    private void Awake()
    {
        // Check if there is more than one instance in the scene
        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of Inventory in the scene");
            return;
        }

        Instance = this;
    }
    #endregion

    /// <summary>
    /// Adds diamonds to the inventory and updates the UI.
    /// </summary>
    public void AddDiamonds()
    {
        m_DiamondsCount ++;
        UpdateDiamondsCountText();
    }

    /// <summary>
    /// Removes diamonds from the inventory and updates the UI.
    /// </summary>
    /// <param name="count">The number of diamonds to remove.</param>
    public void RemoveDiamonds(int count)
    {
        m_DiamondsCount -= count;
        UpdateDiamondsCountText();
    }

    /// <summary>
    /// Updates the UI Diamond Text
    /// </summary>
    private void UpdateDiamondsCountText()
    {
        if (m_DiamondsCountText != null)
        {
            m_DiamondsCountText.text = "x " + m_DiamondsCount.ToString();
        }
    }

}
