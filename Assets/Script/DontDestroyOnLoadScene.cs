using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;
    public static DontDestroyOnLoadScene instance;

    private void Awake()
    {
        // pour v�rifier qu'il n'y a qu'une seule instance de DontDestroyOnLoadScene dans la sc�ne
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Attention, il y a plus d'une instance de DontDestroyOnLoadScene dans la sc�ne");
            Destroy(gameObject);
        }

        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }

    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var element in objects)
        {
            SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
        }
    }
}
