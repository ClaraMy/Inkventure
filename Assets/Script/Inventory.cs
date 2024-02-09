using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int diamondsCount;
    public Text diamondsCountText;
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Attention il y a plus d'une instance Inventory!");
            return;
        }

        instance = this;
    }

    public void AddDiamonds(int count)
    {
        diamondsCount += count;
        diamondsCountText.text = "x " + diamondsCount.ToString();
    }

    public void RemoveDiamonds(int count)
    {
        diamondsCount -= count;
        diamondsCountText.text = "x " + diamondsCount.ToString();
    }

}
