using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false;
    public int diamondsPickedUpInThisSceneCount;
    public static CurrentSceneManager instance;
    private void Awake()
    {
        // pour vérifier qu'il n'y a qu'une seule instance de CurrentSceneManager dans la scène
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Attention, il y a plus d'une instance de CurrentSceneManager dans la scène");
            Destroy(gameObject);
        }
    }
}
