using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    private Resolution[] m_Resolutions;

    [SerializeField] private Dropdown m_ResolutionDropdown;

    public void Start()
    {
        // Get all available resolutions and remove duplicates
        m_Resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        m_ResolutionDropdown.ClearOptions();

        List<string> options = new();

        int currentResolutionsIndex = 0;

        // Loop through resolutions and add them as dropdown options
        for (int i = 0; i < m_Resolutions.Length; i++)
        {
            string option = m_Resolutions[i].width + "x" + m_Resolutions[i].height;
            options.Add(option);

            // Set the index of the current resolution
            if (m_Resolutions[i].width == Screen.width && m_Resolutions[i].height == Screen.height)
            {
                currentResolutionsIndex = i;
            }
        }

        // Add resolution options to the dropdown
        m_ResolutionDropdown.AddOptions(options);
        // Set the current resolution in the dropdown
        m_ResolutionDropdown.value = currentResolutionsIndex;
        // Refresh the shown value of the dropdown
        m_ResolutionDropdown.RefreshShownValue();

        // Set fullscreen mode to true by default
        Screen.fullScreen = true;
    }

    /// <summary>
    /// Toggles fullscreen mode.
    /// </summary>
    /// <param name="isFullScreen">Indicates whether to enable fullscreen mode.</param>
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    /// <summary>
    /// Sets the game resolution based on the selected dropdown option.
    /// </summary>
    /// <param name="resolutionIndex">Index of the selected resolution option.</param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = m_Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Clears all saved player preferences.
    /// </summary>
    public void ClearSavedData()
    {
        PlayerPrefs.DeleteAll();
    }
}
