using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Attention, il y a plus d'une instance de SaveData dans la scène");
            return;
        }

        instance = this;
    }

    public void SaveLevel()
    {
        if (CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
        }
    }
}
