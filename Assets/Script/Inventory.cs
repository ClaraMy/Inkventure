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
    private static Inventory m_Instance;

    public static Inventory Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new Inventory();
            }

            return m_Instance;
        }
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
